using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
namespace INTEX2.Pages
{
    [Authorize(Roles = "Admins")]
    public class AdminPageModel : PageModel
    {
    }
}