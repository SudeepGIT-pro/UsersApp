# Pricing and Calculation System - Implementation Summary

## ✅ Updates Completed

Updated the Request Quotation system to use specific square footage values and rates for each model type, with automatic calculation based on number of units.

## Model Specifications and Pricing

| Model Type | Square Footage | Rate per Unit | Description |
|------------|----------------|---------------|-------------|
| **A FRAME** | 847 sq.ft | ₹21,50,000 | LGS A-Frame Structure |
| **DELTA 1 BHK** | 400 sq.ft | ₹10,50,000 | Delta 1 BHK Structure |
| **DELTA 2 BHK** | 822 sq.ft | ₹20,70,000 | Delta 2 BHK Structure |
| **DELTA 3 BHK** | 1466 sq.ft | ₹30,40,000 | Delta 3 BHK Structure |
| **DELTA STUDIO A** | 410 sq.ft | ₹9,50,000 | Delta Studio A Structure |
| **DELTA STUDIO B** | 445 sq.ft | ₹9,80,000 | Delta Studio B Structure |

## Calculation Examples

### Example 1: A FRAME - 1 Unit
- **Selected Model:** A FRAME
- **Number of Units:** 1
- **Square Footage:** 847 sq.ft
- **Rate per Unit:** ₹21,50,000
- **Total Amount:** ₹21,50,000
- **In Words:** TWENTY ONE LAKH FIFTY THOUSAND ONLY

### Example 2: A FRAME - 5 Units
- **Selected Model:** A FRAME
- **Number of Units:** 5
- **Square Footage:** 847 sq.ft (per unit)
- **Rate per Unit:** ₹21,50,000
- **Calculation:** 21,50,000 × 5
- **Total Amount:** ₹1,07,50,000
- **In Words:** ONE CRORE SEVEN LAKH FIFTY THOUSAND ONLY

### Example 3: DELTA 2 BHK - 3 Units
- **Selected Model:** DELTA 2 BHK
- **Number of Units:** 3
- **Square Footage:** 822 sq.ft (per unit)
- **Rate per Unit:** ₹20,70,000
- **Calculation:** 20,70,000 × 3
- **Total Amount:** ₹62,10,000
- **In Words:** SIXTY TWO LAKH TEN THOUSAND ONLY

### Example 4: DELTA STUDIO A - 10 Units
- **Selected Model:** DELTA STUDIO A
- **Number of Units:** 10
- **Square Footage:** 410 sq.ft (per unit)
- **Rate per Unit:** ₹9,50,000
- **Calculation:** 9,50,000 × 10
- **Total Amount:** ₹95,00,000
- **In Words:** NINETY FIVE LAKH ONLY

## Implementation Details

### Files Modified:

#### 1. **AccountController.cs** (`Controllers/AccountController.cs`)

Added three new methods:

##### A. `CalculateTotalAmount` Method
```csharp
private decimal CalculateTotalAmount(string typeOfModel, int numberOfUnits)
{
    decimal ratePerUnit = typeOfModel switch
    {
        "A FRAME" => 2150000,
        "DELTA 1 BHK" => 1050000,
        "DELTA 2 BHK" => 2070000,
        "DELTA 3 BHK" => 3040000,
        "DELTA STUDIO A" => 950000,
        "DELTA STUDIO B" => 980000,
        _ => 0
    };
    
    return ratePerUnit * numberOfUnits;
}
```

**Purpose:** Calculates the total amount based on model type and number of units.

##### B. `ConvertAmountToWords` Method
```csharp
private string ConvertAmountToWords(decimal amount)
{
    // Converts numerical amount to Indian number system words
    // Handles Crores, Lakhs, Thousands, Hundreds
}
```

**Purpose:** Converts the numerical total to words in Indian numbering format (Lakhs and Crores).

**Features:**
- Handles amounts up to Crores
- Uses Indian numbering system
- Returns format like "TWENTY ONE LAKH FIFTY THOUSAND ONLY"

##### C. Updated `GetMainItems` Method
```csharp
private List<QuotationItem> GetMainItems(string typeOfModel, int numberOfUnits)
{
    var modelSpecs = new Dictionary<string, (int sqft, decimal rate, string description)>
    {
        { "A FRAME", (847, 2150000, "...") },
        { "DELTA 1 BHK", (400, 1050000, "...") },
        // ... etc
    };
    
    decimal totalAmount = spec.rate * numberOfUnits;
    // Returns item with calculated total
}
```

**Purpose:** Creates quotation items with correct square footage, calculated totals, and proper formatting.

## Quotation Display Format

### Description Column:
```
FABRICATION & SUPPLY AND OF LGS A-FRAME STRUCTURE. 
INCLUDES FIXTURES & FINISHES. (847 sq.ft)
```
- Shows model name
- Includes complete description
- Displays square footage in brackets

### Quantity Column:
```
05  (for 5 units)
01  (for 1 unit)
10  (for 10 units)
```
- Two-digit format for single digits (01, 02, etc.)
- Regular format for larger numbers (10, 25, etc.)

### Rate Column:
```
21,50,000 INR    (for 1 unit of A FRAME)
1,07,50,000 INR  (for 5 units of A FRAME)
62,10,000 INR    (for 3 units of DELTA 2 BHK)
```
- Shows total calculated amount (rate × units)
- Formatted with thousand separators
- Displays "INR" currency indicator

### Total in Rupees (Below table):
```
TOTAL IN RUPEES: TWENTY ONE LAKH FIFTY THOUSAND ONLY
TOTAL IN RUPEES: ONE CRORE SEVEN LAKH FIFTY THOUSAND ONLY
```
- Converts numerical total to words
- Uses Indian number system (Lakhs, Crores)
- Ends with "ONLY"

## Number to Words Conversion Examples

| Amount | In Words |
|--------|----------|
| ₹21,50,000 | TWENTY ONE LAKH FIFTY THOUSAND ONLY |
| ₹1,07,50,000 | ONE CRORE SEVEN LAKH FIFTY THOUSAND ONLY |
| ₹62,10,000 | SIXTY TWO LAKH TEN THOUSAND ONLY |
| ₹95,00,000 | NINETY FIVE LAKH ONLY |
| ₹9,50,000 | NINE LAKH FIFTY THOUSAND ONLY |
| ₹2,00,00,000 | TWO CRORE ONLY |

## Features

✅ **Automatic Calculation:** Total = Rate per Unit × Number of Units

✅ **Dynamic Square Footage:** Each model shows its specific sq.ft in description

✅ **Accurate Pricing:** Uses exact rates as specified

✅ **Indian Number Format:** Amount in words uses Lakhs and Crores

✅ **Professional Display:** Formatted with thousand separators

✅ **Flexible Units:** Supports any number of units (1 to unlimited)

✅ **Future-Proof:** Easy to add new models or update prices

## Testing Scenarios

### Test Case 1: Single Unit Orders
- Select any model
- Enter 1 for Number of Units
- Verify total matches the base rate

### Test Case 2: Multiple Units (Same Model)
- Select A FRAME
- Enter 5 for Number of Units
- Expected Total: ₹1,07,50,000
- Expected Words: ONE CRORE SEVEN LAKH FIFTY THOUSAND ONLY

### Test Case 3: Budget Model
- Select DELTA STUDIO A
- Enter 1 for Number of Units
- Expected Total: ₹9,50,000
- Expected Words: NINE LAKH FIFTY THOUSAND ONLY

### Test Case 4: Premium Model with Multiple Units
- Select DELTA 3 BHK
- Enter 3 for Number of Units
- Expected Total: ₹91,20,000
- Expected Words: NINETY ONE LAKH TWENTY THOUSAND ONLY

### Test Case 5: Large Order
- Select DELTA 2 BHK
- Enter 10 for Number of Units
- Expected Total: ₹2,07,00,000
- Expected Words: TWO CRORE SEVEN LAKH ONLY

## How It Works - Step by Step

1. **User selects model type** (e.g., A FRAME)
2. **User enters number of units** (e.g., 5)
3. **System retrieves model specs:**
   - Square footage: 847 sq.ft
   - Rate per unit: ₹21,50,000
4. **System calculates total:**
   - 21,50,000 × 5 = ₹1,07,50,000
5. **System converts to words:**
   - "ONE CRORE SEVEN LAKH FIFTY THOUSAND ONLY"
6. **Quotation displays:**
   - Description: "...FINISHES. (847 sq.ft)"
   - Quantity: "05"
   - Rate: "1,07,50,000 INR"
   - Total in Rupees: "ONE CRORE SEVEN LAKH FIFTY THOUSAND ONLY"

## Benefits

1. **Accuracy:** Eliminates manual calculation errors
2. **Consistency:** Same pricing applies to all quotations
3. **Transparency:** Client sees exact calculation
4. **Professional:** Uses proper number formatting
5. **Scalable:** Easy to add more models or update pricing
6. **User-Friendly:** Automatic calculations save time

## To Update Prices in Future

To change a model's price:

1. Open `AccountController.cs`
2. Find `CalculateTotalAmount` method
3. Update the rate value
4. Find `GetMainItems` method
5. Update the corresponding rate in modelSpecs dictionary

Example:
```csharp
// To change A FRAME price from 2,150,000 to 2,500,000
"A FRAME" => 2500000,  // In CalculateTotalAmount

{ "A FRAME", (847, 2500000, "...") },  // In GetMainItems
```

## Notes

- All prices are in Indian Rupees (INR)
- Square footage is per unit
- The system uses .NET's decimal type for precision
- Number formatting uses thousand separators (Indian style)
- The "ONLY" suffix is standard in Indian quotations

---

**Build Status:** ✅ Successful  
**Ready for Production:** Yes

**To Test:**
1. Stop and restart your application
2. Navigate to Request Quotation page
3. Try different model types and unit quantities
4. Verify calculations in the quotation display
