using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Models;
using UsersApp.ViewModels;

namespace UsersApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    
                    TempData["SuccessMessage"] = "Logged in Successfully";
                    return RedirectToAction("RequestQuotation", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                   
                    TempData["SuccessMessage"] = "Registered Successfully";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
                
            }
            return View(model);
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        public IActionResult RequestQuotation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestQuotation(RequestQuotationViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["ClientName"] = model.ClientName ?? "Valued Client";
                TempData["Location"] = model.Location ?? "";
                TempData["TypeOfModel"] = model.TypeOfModel ?? "";
                TempData["NumberOfUnits"] = model.NumberOfUnits;

                return RedirectToAction("QuotationDisplay");
            }

            return View(model);
        }

        public IActionResult QuotationDisplay()
        {
            var clientName = TempData["ClientName"]?.ToString() ?? "Valued Client";
            var location = TempData["Location"]?.ToString() ?? "";
            var typeOfModel = TempData["TypeOfModel"]?.ToString() ?? "";
            var numberOfUnits = TempData["NumberOfUnits"] != null ? Convert.ToInt32(TempData["NumberOfUnits"]) : 1;
            var totalAmount = CalculateTotalAmount(typeOfModel, numberOfUnits);
            var model = new QuotationDisplayViewModel
            {
                ClientName = clientName,
                Location = location,
                TypeOfModel = typeOfModel,
                NumberOfUnits = numberOfUnits,
                TotalInWords = ConvertAmountToWords(totalAmount),
                MainItems = GetMainItems(typeOfModel, numberOfUnits),
                OptionalItems = GetOptionalItems()
            };

            return View(model);
        }

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

        private string ConvertAmountToWords(decimal amount)
        {
            if (amount == 0) return "ZERO RUPEES ONLY";

            string[] ones = { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
            string[] teens = { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            string[] tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            long number = (long)amount;
            string words = "";

            if (number >= 10000000)
            {
                long crores = number / 10000000;
                words += ConvertTwoDigits(crores, ones, teens, tens) + " CRORE ";
                number %= 10000000;
            }

            if (number >= 100000)
            {
                long lakhs = number / 100000;
                words += ConvertTwoDigits(lakhs, ones, teens, tens) + " LAKH ";
                number %= 100000;
            }

            if (number >= 1000)
            {
                long thousands = number / 1000;
                words += ConvertTwoDigits(thousands, ones, teens, tens) + " THOUSAND ";
                number %= 1000;
            }

            if (number >= 100)
            {
                long hundreds = number / 100;
                words += ones[hundreds] + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                words += ConvertTwoDigits(number, ones, teens, tens);
            }

            return words.Trim() + " ONLY";
        }

        private string ConvertTwoDigits(long number, string[] ones, string[] teens, string[] tens)
        {
            if (number < 10)
                return ones[number];
            else if (number < 20)
                return teens[number - 10];
            else
            {
                long tensDigit = number / 10;
                long onesDigit = number % 10;
                return tens[tensDigit] + (onesDigit > 0 ? " " + ones[onesDigit] : "");
            }
        }

        private List<QuotationItem> GetMainItems(string typeOfModel, int numberOfUnits)
        {
            var items = new List<QuotationItem>();

            var modelSpecs = new Dictionary<string, (int sqft, decimal rate, string description)>
            {
                { "A FRAME", (847, 2150000, "FABRICATION & SUPPLY AND OF LGS A-FRAME STRUCTURE. INCLUDES FIXTURES & FINISHES.") },
                { "DELTA 1 BHK", (400, 1050000, "FABRICATION & SUPPLY OF DELTA 1 BHK STRUCTURE. INCLUDES FIXTURES & FINISHES.") },
                { "DELTA 2 BHK", (822, 2070000, "FABRICATION & SUPPLY OF DELTA 2 BHK STRUCTURE. INCLUDES FIXTURES & FINISHES.") },
                { "DELTA 3 BHK", (1466, 3040000, "FABRICATION & SUPPLY OF DELTA 3 BHK STRUCTURE. INCLUDES FIXTURES & FINISHES.") },
                { "DELTA STUDIO A", (410, 950000, "FABRICATION & SUPPLY OF DELTA STUDIO A STRUCTURE. INCLUDES FIXTURES & FINISHES.") },
                { "DELTA STUDIO B", (445, 980000, "FABRICATION & SUPPLY OF DELTA STUDIO B STRUCTURE. INCLUDES FIXTURES & FINISHES.") }
            };

            if (modelSpecs.ContainsKey(typeOfModel))
            {
                var spec = modelSpecs[typeOfModel];
                decimal totalAmount = spec.rate * numberOfUnits;

                items.Add(new QuotationItem
                {
                    SerialNo = 1,
                    Description = $"{spec.description} ({spec.sqft} sq.ft)",
                    Unit = "LS",
                    Quantity = numberOfUnits.ToString("D2"),
                    Rate = totalAmount.ToString("N0") + " INR"
                });
            }
            else
            {
                items.Add(new QuotationItem
                {
                    SerialNo = 1,
                    Description = $"FABRICATION & SUPPLY OF {typeOfModel} STRUCTURE. INCLUDES FIXTURES & FINISHES.",
                    Unit = "LS",
                    Quantity = numberOfUnits.ToString("D2"),
                    Rate = "Price on Request"
                });
            }

            return items;
        }

        private List<QuotationItem> GetOptionalItems()
        {
            return new List<QuotationItem>
            {
                new QuotationItem
                {
                    SerialNo = 1,
                    Description = "SOIL TEST",
                    Unit = "LS",
                    Quantity = "01",
                    Rate = "36,000 INR"
                },
                new QuotationItem
                {
                    SerialNo = 2,
                    Description = "RR FOUNDATION WITH RAFT SLAB",
                    Unit = "LS",
                    Quantity = "01",
                    Rate = "3,95,000 INR"
                },
                new QuotationItem
                {
                    SerialNo = 3,
                    Description = "ISOLATED FOUNDATION (READYMADE FOUNDATION)",
                    Unit = "LS",
                    Quantity = "01",
                    Rate = "3,70,000 INR"
                },
                new QuotationItem
                {
                    SerialNo = 4,
                    Description = "LOOSE FURNITURES",
                    Unit = "LOT",
                    Quantity = "01",
                    Rate = "1,00,000 INR"
                }
            };
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["WarningMessage"] = "Logout Successfully";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ViewProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.FullName = user.FullName;
            ViewBag.Email = user.Email;
            return View();
        }
    }
}
