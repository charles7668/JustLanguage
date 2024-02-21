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
        articleInfo.UploadTime = DateTime.Now;
        EntityEntry<ArticleInfo> result = await _context.ArticleInfo.AddAsync(articleInfo);
        if (result.State != EntityState.Added)
        {
            return false;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ArticleInfoDTO>> GetArticles(int offset = 0, int limit = 20)
    {
        List<ArticleInfoDTO> articleInfoDtoList = await _context.ArticleInfo.Skip(offset).Take(limit)
            .Select(info => _mapper.Map<ArticleInfoDTO>(info)).ToListAsync();
        return articleInfoDtoList;
    }

    /// <inheritdoc />
    public Task<bool> HasDuplicateArticle(UploadArticleDTO uploadArticleDto)
    {
        return _context.ArticleInfo.AnyAsync(x => x.SrcUrl == uploadArticleDto.ArticleUrl);
    }
}