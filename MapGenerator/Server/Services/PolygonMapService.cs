using Microsoft.EntityFrameworkCore;
using Server.Entities.Entities;
using Server.Entities.Mappers;
using Server.Models;
using Server.Response;
using Shared.DTO;
using Shared.Response.ErrorRespond;
using Shared.Response.IResponse;
using Shared.Response.SuccessRespond;

namespace Server.Services;

public class PolygonMapService(DataContext dataContext)
{
    public async Task<HandlerResult<Success, IErrorResult>> AddPolygonMap(PolygonMapDto polygonMapDto,long userId)
    {
        var map = PolygonMapMapper.DtoToPolygonMap(polygonMapDto, userId);
        dataContext.PolygonMaps.Add(map);
        foreach (var heightPoint in map.HeightPoints!)
        {
            heightPoint.PolygonMap = map;
            dataContext.HeightPoints.Add(heightPoint);
        }

        foreach (var humidityPoint in map.HumidityPoints!)
        {
            humidityPoint.PolygonMap = map;
            dataContext.HumidityPoints.Add(humidityPoint);
        }
        await dataContext.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<List<PolygonMapDto>>, IErrorResult>>GetAll(long userId)
    {

        var res = await dataContext.PolygonMaps.Where(x => x.UserId == userId)
            .Include(x=>x.HumidityPoints)
            .Include(x=>x.HeightPoints)
            .Select(x=> PolygonMapMapper.PolygonMapToDto(x)).ToListAsync();
        await dataContext.SaveChangesAsync();
        return new SuccessData<List<PolygonMapDto>>()
        {
            Data = res
        };
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> DeletePolygonMap(long mapId,long userId)
    {

        var res = await dataContext.PolygonMaps.FirstOrDefaultAsync(x => x.Id == mapId && userId == x.UserId);
        if (res == null) return new EntityNotFound();
        
        dataContext.PolygonMaps.Remove(res);
        await dataContext.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<PolygonMapDto>, IErrorResult>> GetPolygonMap(long mapId,long userId)
    {

        var res = await dataContext.PolygonMaps.Where(x => x.Id == mapId && userId == x.UserId)
            .Include(x=>x.HumidityPoints)
            .Include(x=>x.HeightPoints).FirstOrDefaultAsync();
        if (res == null) return new EntityNotFound();
        
        return new SuccessData<PolygonMapDto>()
        {
            Data = PolygonMapMapper.PolygonMapToDto(res)
        };
    }
}