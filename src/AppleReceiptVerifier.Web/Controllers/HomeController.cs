using System.Threading.Tasks;
using System.Web.Mvc;
using AppleReceiptVerifier.Web.Models;

namespace AppleReceiptVerifier.Web.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>returns ActionResult</returns>
        public ActionResult Index()
        {
            ReceiptTestViewModel model = new ReceiptTestViewModel();
            model.Environment = "production";
            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>returns ActionResult</returns>
        [HttpPost]
        public async Task<ActionResult> Index(ReceiptTestViewModel model)
        {
            ReceiptManager receiptManager = new ReceiptManager();
            var env = AppleReceiptVerifier.Environments.Production;
            if (model.Environment == "sandbox")
            {
                env = AppleReceiptVerifier.Environments.Sandbox;
            }

            if (model.UseAsync)
            {
                model.ReceiptResponse = await receiptManager.ValidateReceiptAsync(env, model.ReceiptData, model.Password);
            }
            else
            {
                model.ReceiptResponse = receiptManager.ValidateReceipt(env, model.ReceiptData, model.Password);
            }

            return this.View(model);
        }
    }
}
