# Stress test data generation script
# Creates sample data files with 1000, 10000, and 100000 items in CSV format

$dataDir = Join-Path (Get-Location) "data"
if (-not (Test-Path $dataDir)) {
    New-Item -ItemType Directory -Path $dataDir | Out-Null
}

# Sample item templates for variety
$itemTemplates = @(
    @{ name = "Great Value Whole Milk"; basePrice = 3.28; baseQty = 120 },
    @{ name = "Wonder Classic White Bread"; basePrice = 2.48; baseQty = 80 },
    @{ name = "Lay's Classic Potato Chips"; basePrice = 4.29; baseQty = 60 },
    @{ name = "Great Value Large Eggs"; basePrice = 2.29; baseQty = 150 },
    @{ name = "Coca-Cola 12 Pack"; basePrice = 6.98; baseQty = 90 },
    @{ name = "Bounty Paper Towels"; basePrice = 12.97; baseQty = 45 },
    @{ name = "Charmin Ultra Soft Toilet Paper"; basePrice = 14.97; baseQty = 50 },
    @{ name = "Hanes Mens T-Shirt 5-Pack"; basePrice = 17.84; baseQty = 35 },
    @{ name = "George Mens Jeans"; basePrice = 19.97; baseQty = 40 },
    @{ name = "Mainstays Bath Towel"; basePrice = 4.88; baseQty = 70 },
    @{ name = "Ozark Trail Stainless Steel Tumbler"; basePrice = 9.94; baseQty = 55 },
    @{ name = "Samsung 32-inch Smart TV"; basePrice = 158.00; baseQty = 12 },
    @{ name = "Duracell AA Batteries"; basePrice = 14.24; baseQty = 65 },
    @{ name = "Crayola Crayons 24 Count"; basePrice = 1.47; baseQty = 200 },
    @{ name = "LEGO Classic Medium Brick Box"; basePrice = 34.76; baseQty = 25 }
)

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
            $name = "$($template.name) #$i"
            $price = [math]::Round($template.basePrice * (0.8 + (Get-Random -Minimum 0 -Maximum 200) / 100), 2)
            $qty = $template.baseQty + (Get-Random -Minimum -20 -Maximum 50)
            
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
