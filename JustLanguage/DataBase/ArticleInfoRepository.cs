using AutoMapper;
using JustLanguage.DTOs;
using JustLanguage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JustLanguage.DataBase;

public class ArticleInfoRepository : IArticleInfoRepository
{
    public ArticleInfoRepository(AppDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public async Task<bool> AddArticle(ArticleInfoDTO articleInfoDto)
    {
        var articleInfo = _mapper.Map<ArticleInfo>(articleInfoDto);
        EntityEntry<ArticleInfo> result = await _context.ArticleInfo.AddAsync(articleInfo);
        if (result.State != EntityState.Added)
        {
            return false;
        }

        await _context.SaveChangesAsync();
        return true;
    }
}