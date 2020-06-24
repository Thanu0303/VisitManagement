using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VisitorManagement.DataAccess;
using VisitorManagement.DataAccess.Models;
using VisitorManagement.DataAccess.ViewModel;


namespace Main.Controllers
{
    public class AccountController : Controller
    {
        private VisitorManagementContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public AccountController(VisitorManagementContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Login()
        {
            return View();
        }

        //this function isto Encode your Password
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function isto Decode your Password
        public string DecodeFrom64(string encodedData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            DataBaseHelper dataBaseHelper = new DataBaseHelper(_context);

            if (ModelState.IsValid)
            {
                _context = new VisitorManagementContext();

                var userdetails = _context.User
                .LastOrDefault(m => m.UserLogin == model.UserLogin);
                string decryptpassword = userdetails.Password;
                userdetails.Password = DecodeFrom64(decryptpassword);

                HttpContext.Session.SetString("UserID", userdetails.UserID.ToString());
                HttpContext.Session.SetString("IsAdmin", userdetails.IsAdmin.ToString());
                HttpContext.Session.SetString("UserName", userdetails.FirstName.ToString());

                if (userdetails.Password == model.Password)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
                else if (userdetails.Password != model.Password)
                {
                    ModelState.AddModelError("Password", "Invalid Username or Password..!!");
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult GetAllUsers()
        {
            List<User> users = _context.User.ToList();
            return View(_context.User.ToList().Where(x => x.IsDeleted == false).OrderByDescending(r => r.UserID));

        }

        public IActionResult GetUserById(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            User user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = _context.User.Where(x => x.UserLogin == newUser.UserLogin).FirstOrDefault();
                    if (obj != null)
                    {
                        ViewBag.UserLogin = "User already Exists";
                        return View("CreateUser");
                    }
                    User user = new User();
                    user.UserLogin = newUser.UserLogin;
                    user.FirstName = newUser.FirstName;
                    user.LastName = newUser.LastName;
                    user.EmailAddress = newUser.EmailAddress;
                    var encryptedPassword = EncodePasswordToBase64(newUser.Password);
                    user.Password = encryptedPassword;
                    user.IsAdmin = newUser.IsAdmin;
                    user.IsDeleted = false;
                    user.IsInactive = newUser.IsInactive;
                    user.IsInactive = newUser.IsInactive;
                    user.CreatedBy = newUser.CreatedBy; // int.Parse(HttpContext.Session.GetString("UserID"));
                    user.ModifiedBy = newUser.ModifiedBy;//int.Parse(HttpContext.Session.GetString("UserID"));
                    _context.User.Add(user);                  
                    _context.SaveChanges();
                    return RedirectToAction("GetAllUsers", "Account");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var editedUser = _context.User.Where(a => a.UserID == id).FirstOrDefault();
            string decryptpassword = editedUser.Password;
            editedUser.Password = DecodeFrom64(decryptpassword);
            return View(editedUser);
        }

        [HttpPost]
        [ActionName("EditUser")]
        public IActionResult Edit_PostUser(User updatedUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _context.User.Where(c => c.UserID == updatedUser.UserID).FirstOrDefault();
                    user.UserLogin = updatedUser.UserLogin;
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.EmailAddress = updatedUser.EmailAddress;
                    var encryptedPassword = EncodePasswordToBase64(updatedUser.Password);
                    user.Password = encryptedPassword;
                    user.IsAdmin = updatedUser.IsAdmin;
                    user.IsInactive = updatedUser.IsInactive;

                    _context.User.Update(user);
                    _context.SaveChanges();
                    return RedirectToAction("GetAllUsers", "Account");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult Delete(int id)
        {
            try
            {
                _context = new VisitorManagementContext();
                var deletedUser = _context.User.FirstOrDefault(x => x.UserID == id);
                if (deletedUser != null)
                {
                    deletedUser.IsDeleted = true;
                    _context.SaveChanges();
                }

                return Json(deletedUser);
            }
            catch (Exception)
            {
                //throw ex;
                return Json(false);
            }
        }

        //export user
        [HttpGet]
        public ActionResult Export()
        {
            return View();
        }

        public ActionResult Download()
        {
            string Files = "wwwroot/Template.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
            System.IO.File.WriteAllBytes(Files, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "UserTemplate.xlsx");
        }

        //import User and save to db   
        public ActionResult OnPostSaveExcel()
        {
            var sheet = ParseSheetFromRequest(false);
            var records = ParseDocsFromSheet(sheet);

            // typically, we'll use Database to generate the Id
            //     as we cannot trust user 

            StringBuilder sb = new StringBuilder();
            int success = 0;
            List<User> failedIndexes = new List<User>();         
            for (int i = 0; records.Count > i; i++)
            {
                bool result = _context.User.Where(x => x.UserLogin == records[i].UserLogin).Count() > 0;
                bool isEmail = Regex.IsMatch(records[i].UserLogin, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isEmail && !result)
                {
                    records[i].UserID = default(int);
                    records[i].Password = EncodePasswordToBase64(records[i].Password);
                    success++;
                }
                else
                {
                    failedIndexes.Add(records[i]);                  
                    if (result)
                    {
                        sb.AppendLine("  User with this Emailaddress " + records[i].UserLogin + " already Exists. <br /> ");
                    }
                    else{
                        sb.AppendLine("  Invalid EmailAddress for the user : " + records[i].FirstName + " " + records[i].LastName + " <br /> ");
                    }
                    

                }
            }
            foreach (var user in failedIndexes)
            {
                records.Remove(user);
            }           
            _context.User.AddRange(records);
            _context.SaveChanges();

            JsonObject obj = new JsonObject();
            obj.Add("successcount", success.ToString() + " Users has been uploaded successfully  <br /> ");
            obj.Add("failurecount", failedIndexes.Count.ToString() + " Users upload failure.  <br /> ");
            obj.Add("FailedUsers", sb.ToString());
            return Ok(obj);
        }

        private ISheet ParseSheetFromRequest(bool saveFile)
        {
            ISheet sheet = null;
            IFormFile file = Request.Form.Files[0];
            if (file.Length == 0)
            {
                return sheet;
            }

            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
            var stream = file.OpenReadStream();
            if (sFileExtension == ".xls")
            {
                HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
            }
            else
            {
                XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
            }

            var records = this.ParseDocsFromSheet(sheet);

            // if need to save the file
            if (saveFile)
            {
                stream = file.OpenReadStream();
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return sheet;
        }

        private List<User> ParseDocsFromSheet(ISheet sheet)
        {
            IRow headerRow = sheet.GetRow(0); //Get Header Row
            int cellCount = headerRow.LastCellNum;
            // ["Id","LastName","","UserName","","Name"]
            var headerNames = new List<string>();
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
                {
                    headerNames.Add("");  // add empty string if cell is empty
                }
                else
                {
                    headerNames.Add(cell.ToString());
                }
            }

            var records = new List<User>();

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }

                if (row.Cells.All(d => d.CellType == CellType.Blank))
                {
                    continue;
                }

                var record = new User();
                var type = typeof(User);
                for (int j = 0; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        var field = row.GetCell(j).ToString();
                        var fieldName = headerNames[j];
                        if (String.IsNullOrWhiteSpace(fieldName))
                        {
                            throw new Exception($"There's a value in Cell({i},{j}) but has no header !");
                        }
                        var pi = type.GetProperty(fieldName);
                        // for Id column : a int type
                        if (pi.PropertyType.IsAssignableFrom(typeof(Int32)))
                        {
                            pi.SetValue(record, Convert.ToInt32(field));
                        }
                        else if (pi.PropertyType.IsAssignableFrom(typeof(Boolean)))
                        {
                            pi.SetValue(record, Convert.ToBoolean(field));
                        }
                        // for other colun : string
                        else
                        {
                            pi.SetValue(record, field);
                        }
                    }
                }
                records.Add(record);
            }
            return records;
        }


    }
}

