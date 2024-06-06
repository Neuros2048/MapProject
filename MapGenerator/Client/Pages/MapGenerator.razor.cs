using Client.SubPages;
using Microsoft.AspNetCore.Components;
using Shared.DTO;

namespace Client.Pages;

public partial class MapGenerator
{
    	private readonly string tileKey = "tile_";
	private bool randomSeed = false;
    private TileSetDto CurrSetDto = new TileSetDto();
    
    private ElementReference container;
    private CanvasPage _context;
    private TileWeightSetting? _tileWeightSetting;
    private GeneratedMapDto Map = new GeneratedMapDto();
    private TileMapSolver? _tileMapSolver;
   // private bool ok = false;
    private bool _mapIsGenereted = false;
    private bool _canDraw = true;
    private async void SetCurrSet(TileSetDto tileSetDto)
    {
	    
	    Map = await MapS.PrepareNewMap(tileSetDto.Id);
	    Map.seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
	    await MapS.GetBaseTile();
	    await MapS.GetTilesToHash(Map.TileSetId);
	    CurrSetDto = tileSetDto;
	    Map.N = 30;
	    Map.M = 30;
	    Map.Name = "Nowa Mapa";
	    await PrepareMap();
    }

   
    
    
    protected async Task PrepareMap()
    {
	    await ResetStart();
	    ResetMassage();
	    _mapIsGenereted = false;
	    
	    Map.SetTileDtos = new List<SetTileDto>();
	    this.StateHasChanged();
	    await _context.Resize(Map.M * 10 , Map.N * 10);
	    await ResetEnd();
	    await _context.Clear();


    }

    private long IdToset = 0;
    private bool tileClicked = false;

    private async void TileClick(long id)
    {
	    IdToset = id;
	    tileClicked = true;
	    this.StateHasChanged();
    }
    private async void TileUnClick()
    {
	    IdToset = 0;
	    tileClicked = false;
    }
    private async void OnMouseClick(CanvasPage.Pos pos)
    {
	    if (tileClicked && ! _mapIsGenereted)
	    {


		    pos.X /= 10;
		    pos.Y /= 10;
		    pos.X = pos.X < 0 ? 0 : pos.X;
		    pos.Y = pos.Y < 0 ? 0 : pos.Y;
		    pos.X = pos.X >= Map.M ? Map.M - 1 : pos.X;
		    pos.Y = pos.Y >= Map.N ? Map.N - 1 : pos.Y;
		    await _context.SetTile(pos.X, pos.Y, IdToset, 10);
		    Map.SetTileDtos.Add(new SetTileDto()
		    {
			    N = pos.Y, M = pos.X, TileId = IdToset
		    });
	    }
    }

    private async void SetTile(SetTileDto setTileDto)
    {
	    if (_canDraw)
	    {
		    await _context.SetTile(setTileDto.M, setTileDto.N, setTileDto.TileId,10);    
	    }
	        
    }

    private async void GenerateMap()
    {
	    await ResetStart();
	    ResetMassage();
	    if (randomSeed)
	    {
		    Map.seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
	    }
	   
	    await Task.Delay(1);
	    await ResetEnd();
	    if (_mapIsGenereted)
	    {
		    await _context.Clear();
		    await _tileMapSolver.SetedTiles();
	    }

	    _mapIsGenereted = true;
	    await _tileMapSolver.GenerateMap();
    }
    
    private async Task SaveMap()
    {
	    var res =await MapS.SaveGeneratedMap(Map);
	    if (res)
	    {
		    message = "Udało się zapisac mape";
	    }
	    else
	    {
		    message = "Nie udało się zapisać mapy";
	    }
    }
    private string message = string.Empty;
    
    private void ResetMassage()
    {
	    message = string.Empty;
    }
    
    private async Task ResetStart()
    {
	    _tileMapSolver.Stop();
	    
	    _canDraw = false;
	    this.StateHasChanged();
	    await Task.Delay(1);
	   
	    

    }
    private async Task ResetEnd()
    {

	   
	    _canDraw = true;
	    this.StateHasChanged();
	
    }

    public void Dispose()
    {
	    _canDraw = false;
    }
    private void RandomSeed()
    {
	    randomSeed = !randomSeed;
    }

}
