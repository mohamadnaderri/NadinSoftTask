using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoftTask.Commands.Product;
using NadinSoftTask.Infrastructure;
using NadinSoftTask.QueryModels.Implementations;
using System.Security.Claims;

namespace NadinSoftTask.Host.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        protected UserInfo CurrentUser => (User.Identity as ClaimsIdentity).Claims.ToList()?.ToUserInfo();

        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// دریافت جزئیات محصول 
        /// </summary>
        /// <param name="id">شناسه محصول</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetProductQueryModel(id));
            return Ok(result);
        }

        [AllowAnonymous]
        /// <summary>
        /// دریافت لیست محصولات 
        /// </summary>
        /// <param name="operatorName">کاربر ایجاد کننده</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string? operatorName)
        {
            var result = await _sender.Send(new GetAllProductQueryModel(operatorName));
            return Ok(result);
        }

        /// <summary>
        /// ثبت محصول
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            command.SetCommandSenderInfo(CurrentUser);
            command.Validate();
            await _sender.Send(command);

            return Ok("عملیات با موفقیت انجام شد");
        }

        /// <summary>
        /// ویرایش محصول
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            command.Id = id;
            command.SetCommandSenderInfo(CurrentUser);
            command.Validate();
            await _sender.Send(command);

            return Ok("عملیات با موفقیت انجام شد");
        }

        /// <summary>
        /// حذف محصول
        /// </summary>
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            command.SetCommandSenderInfo(CurrentUser);
            command.Validate();
            await _sender.Send(command);

            return Ok("عملیات با موفقیت انجام شد");
        }


        /// <summary>
        /// حذف منطقی
        /// </summary>
        [HttpDelete("{id}/soft-delete")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var command = new SoftDeleteProductCommand { Id = id };
            command.SetCommandSenderInfo(CurrentUser);
            command.Validate();
            await _sender.Send(command);

            return Ok("عملیات با موفقیت انجام شد");
        }
    }
}