using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Comments;
using Blog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Blog.Entity.Entities;

namespace Blog.Web.Areas.User.Controllers.CommentController;

[Area("User")]
[Route("User/[controller]/[action]")]

public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IToastNotification _toastNotification;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CommentController(ICommentService commentService, IToastNotification toastNotification, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _commentService = commentService;
        _toastNotification = toastNotification;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpPost]
    public async Task<IActionResult> SendComment(Guid articleId, CommentAddDto commentAddDto)
    {
    
        if (ModelState.IsValid)
        {
            var map = _mapper.Map<Comment>(commentAddDto);
            map.ArticleId = articleId;
            await _unitOfWork.GetRepository<Comment>().AddAsync(map);
            await _unitOfWork.SaveAsync();
            _toastNotification.AddSuccessToastMessage("Yorumunuz başarıyla gönderildi!");
        }
        else
        {
            _toastNotification.AddErrorToastMessage("Yorumunuz gönderilemedi.");
        }

        return RedirectToAction("ArticleDetail", "Home", new {area="", articleId});
    }
}