# Stress test data generation script
# Creates sample data files with 1000, 10000, and 100000 items in CSV format

$dataDir = Join-Path (Get-Location) "data"
if (-not (Test-Path $dataDir)) {
    New-Item -ItemType Directory -Path $dataDir | Out-Null
}

# Load real product data from CSV file (sample every Nth row to avoid memory issues)
$csvPath = "WMT_Grocery_202209.csv"
if (-not (Test-Path $csvPath)) {
    Write-Host "Error: CSV file '$csvPath' not found. Please ensure it's in the current directory."
    exit 1
}

Write-Host "Loading product data from CSV (sampling)..."
$itemTemplates = @()
$lineNum = 0
$samplingInterval = 100  # Sample every 100th row to keep memory usage low

try {
    $reader = [System.IO.StreamReader]::new($csvPath)
    # Skip header
    $reader.ReadLine() | Out-Null
    
    while (($line = $reader.ReadLine()) -ne $null) {
        $lineNum++
        
        # Sample every Nth line
        if ($lineNum % $samplingInterval -eq 0) {
            $fields = $line -split '(?<!\\),'  # Split on commas not within quotes
            # PRODUCT_NAME is field 8 (index 7), PRICE_CURRENT is field 11 (index 10)
            if ($fields.Count -gt 10) {
                $productName = $fields[7].Trim('"')
                $priceStr = $fields[10].Trim('"')
                
                if ($productName -and $priceStr -match '^\d+(\.\d{2})?$') {
                    $itemTemplates += @{
                        name = $productName
                        basePrice = [double]$priceStr
                        baseQty = Get-Random -Minimum 20 -Maximum 150
                    }
                }
            }
        }
    }
    $reader.Dispose()
    
    Write-Host "Loaded $($itemTemplates.Count) products from CSV (sampled 1 per $samplingInterval rows)"
}
catch {
    Write-Host "Error reading CSV file: $_"
    exit 1
}

if ($itemTemplates.Count -eq 0) {
    Write-Host "No valid product data found in CSV file."
    exit 1
}

function GenerateTestFile {
    param([int]$itemCount, [string]$filename)
    
    Write-Host "Generating $filename with $itemCount items..."
    $startTime = Get-Date
    
    $filePath = Join-Path $dataDir $filename
    
    # Use StreamWriter for better performance with large files
    $writer = [System.IO.StreamWriter]::new($filePath, $false, [System.Text.Encoding]::UTF8, 65536)
    
    try {
        for ($i = 1; $i -le $itemCount; $i++) {
            $template = $itemTemplates[($i - 1) % $itemTemplates.Count]
            $name = $template.name
            $price = [math]::Round($template.basePrice * (0.9 + (Get-Random -Minimum 0 -Maximum 20) / 100), 2)
            $qty = $template.baseQty + (Get-Random -Minimum -10 -Maximum 30)
            
            $line = "$name, $price, $qty"
            $writer.WriteLine($line)
            
            # Progress indicator every 10000 items
            if ($i % 10000 -eq 0) {
                Write-Host "  Generated $i items..."
            }
        }
    }
    finally {
        $writer.Dispose()
    }
    
    $endTime = Get-Date
    $duration = ($endTime - $startTime).TotalSeconds
    Write-Host "[DONE] Completed $filename in $($duration)s"
}

# Generate test files
Write-Host "Creating stress test data files..."
Write-Host ""

GenerateTestFile -itemCount 1000 -filename "sample_items_1000.txt"
Write-Host ""

GenerateTestFile -itemCount 10000 -filename "sample_items_10000.txt"
Write-Host ""

GenerateTestFile -itemCount 100000 -filename "sample_items_100000.txt"
Write-Host ""

# Verify the files
Write-Host "Verifying generated files..."
Write-Host ""

$filesToCheck = @(
    @{ name = "sample_items_1000.txt"; expectedLines = 1000 },
    @{ name = "sample_items_10000.txt"; expectedLines = 10000 },
    @{ name = "sample_items_100000.txt"; expectedLines = 100000 }
)

$allValid = $true
foreach ($fileInfo in $filesToCheck) {
    $filePath = Join-Path $dataDir $fileInfo.name
    if (Test-Path $filePath) {
        $lineCount = (Get-Content $filePath -ReadCount 0).Count
        if ($lineCount -eq $fileInfo.expectedLines) {
            Write-Host "[PASS] $($fileInfo.name): $lineCount lines (expected $($fileInfo.expectedLines))"
        }
        else {
            Write-Host "[FAIL] $($fileInfo.name): $lineCount lines (expected $($fileInfo.expectedLines))"
            $allValid = $false
        }
    }
    else {
        Write-Host "[FAIL] $($fileInfo.name): File not found"
        $allValid = $false
    }
}

Write-Host ""
if ($allValid) {
    Write-Host "All stress test files generated successfully!"
}
else {
    Write-Host "Some files did not generate correctly."
}
