using CleanCode.Business.Interfaces;
using CleanCode.Business.Mapper;
using CleanCode.Business.Models;
using CleanCode.Core.Communication;
using CleanCode.Core.Entities;
using CleanCode.Core.Models;
using CleanCode.Core.Repositories;
using CleanCode.Util.Logging;
using Microsoft.Extensions.Logging;

namespace CleanCode.Business.Services
{
    //public class MetadataService : IMetadataService
    //{
    //    private readonly IMetadataRepository _metadataRepository;
    //    private readonly ILogger<MetadataService> _logger;
    //    private readonly IOrderServiceProxy _orderCommunication;

    //    public MetadataService(IMetadataRepository metadataRepository, ILogger<MetadataService> logger)
    //    {
    //        _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //    }

    //    public MetadataService(IMetadataRepository metadataRepository, IOrderServiceProxy orderCommunication, ILogger<MetadataService> logger)
    //    {
    //        _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
    //        _orderCommunication = orderCommunication ?? throw new ArgumentNullException(nameof(orderCommunication));
    //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    //    }

    //    public async Task<PagedList<object>> Get(string tableName)
    //    {
    //        //Example of using Infrastructure Layer's MetadataRepository with Specification Pattern
    //        var metadataList = await _metadataRepository.Get(tableName);
    //        return ObjectMapper.Mapper.Map<PagedList<Object>>(metadataList);
    //    }

    //    public async Task<PagedList<object>> Get(string tableName, string filtercolumn, string filterValue)
    //    {
    //        var metadataList = await _metadataRepository.Get(tableName, filtercolumn, filterValue);
    //        return ObjectMapper.Mapper.Map<PagedList<Object>>(metadataList);
    //    }

    //    public async Task<PagedList<object>> Get(string tableName, string whereCondition)
    //    {
    //        var metadataList = await _metadataRepository.Get(tableName, whereCondition);
    //        return ObjectMapper.Mapper.Map<PagedList<Object>>(metadataList);
    //    }
    //}
}
