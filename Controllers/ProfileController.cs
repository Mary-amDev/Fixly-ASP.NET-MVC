using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fixly.Data;
using Fixly.Models;

namespace Fixly.Controllers
{
    [Authorize] 
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(UserManager<ApplicationUser> userManager, AppDbContext context, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            _environment = environment;
        }

        //  تعرض صفحة البروفايل
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault();

            var model = new ProfileViewModel();
            model.FullName = user.FullName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.City = user.City;
            model.ProfilePicturePath = user.ProfilePicturePath;
            model.Role = role;

            if (role == "Service Provider")
            {
                var providerProfile = await _context.ServiceProviderProfiles
                    .Include(p => p.WorkImages)
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (providerProfile != null)
                {
                    model.ServiceCategory = providerProfile.ServiceCategory;
                    model.YearsExperience = providerProfile.YearsExperience;
                    model.About = providerProfile.About;
                    model.WorkImages = providerProfile.WorkImages.ToList();
                }
            }

            return View(model);
        }

        
        // صفحة  edit
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault();

            var model = new ProfileEditViewModel();
            model.Role = role;
            model.FullName = user.FullName;
            model.PhoneNumber = user.PhoneNumber;
            model.City = user.City;
            model.CurrentProfilePicturePath = user.ProfilePicturePath;

            if (role == "Service Provider")
            {
                var providerProfile = await _context.ServiceProviderProfiles
                    .Include(p => p.WorkImages)
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (providerProfile != null)
                {
                    model.ServiceCategory = providerProfile.ServiceCategory;
                    model.YearsExperience = providerProfile.YearsExperience;
                    model.About = providerProfile.About;
                    model.ExistingWorkImages = providerProfile.WorkImages.ToList();
                }
            }

            return View(model);
        }

        // حفظ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault();
            model.Role = role;

            
            if (role == "Service Provider")
            {
                if (string.IsNullOrWhiteSpace(model.ServiceCategory))
                    ModelState.AddModelError("ServiceCategory", "Service category is required.");
            }

            
            if (!ModelState.IsValid)
            {
                if (role == "Service Provider")
                {
                    var pp = await _context.ServiceProviderProfiles
                        .Include(p => p.WorkImages)
                        .FirstOrDefaultAsync(p => p.UserId == user.Id);
                    if (pp != null)
                        model.ExistingWorkImages = pp.WorkImages.ToList();
                }
                model.CurrentProfilePicturePath = user.ProfilePicturePath;
                return View(model);
            }

            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.City = model.City;

            //حفظ الصورة 
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "profiles");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePicturePath = "images/profiles/" + fileName;
            }

            await _userManager.UpdateAsync(user);

            
            if (role == "Service Provider")
            {
                var providerProfile = await _context.ServiceProviderProfiles
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (providerProfile == null)
                {
                    providerProfile = new ServiceProviderProfile();
                    providerProfile.UserId = user.Id;
                    _context.ServiceProviderProfiles.Add(providerProfile);
                }

                providerProfile.ServiceCategory = model.ServiceCategory;
                providerProfile.YearsExperience = model.YearsExperience;
                providerProfile.About = model.About;

                await _context.SaveChangesAsync();

                
                if (model.NewWorkImages != null && model.NewWorkImages.Count > 0)
                {
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "work");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    foreach (var image in model.NewWorkImages)
                    {
                        if (image.Length > 0)
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string filePath = Path.Combine(uploadFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            var newImage = new WorkImage();
                            newImage.ServiceProviderProfileId = providerProfile.Id;
                            newImage.ImagePath = "images/work/" + fileName;
                            _context.WorkImages.Add(newImage);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkImage(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var image = await _context.WorkImages
                .Include(w => w.ServiceProviderProfile)
                .FirstOrDefaultAsync(w => w.Id == id);

            
            if (image != null && image.ServiceProviderProfile.UserId == user.Id)
            {
                _context.WorkImages.Remove(image);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit");
        }
    }
}