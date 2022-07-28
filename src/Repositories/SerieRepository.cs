using CsharpApiRest.Interfaces;

namespace CsharpApiRest.Models;

public class SeriesRepository : IRepository<SerieModel>
{
    private Dictionary<int, SerieModel> _Series = new();
    private int _lastValue = 0;    

    public void Create(SerieModel genericObject)
    {
        genericObject.Id = _lastValue;
        _Series.Add(_lastValue++, genericObject);
    }

    public List<SerieModel> ReadAll() => _Series.Values.ToList();

    public bool ReadById(int id, out SerieModel? serieModel) => _Series.TryGetValue(id, out serieModel);

    public bool Update(int id, SerieModel genericObject)
    {
        var valueExists = _Series.TryGetValue(id, out var _);

        if (!valueExists)
        {
            return false;
        }

        _Series[id] = genericObject;
        return true;
    }

    public void DeleteAll() => _Series.Clear();

    public bool DeleteById(int id)
    {
        var valueExists = _Series.TryGetValue(id, out var _);

        if (!valueExists)
        {
            return false;
        }

        _Series[id].Deleted = true;
        _Series[id].DeletedAt = DateTime.Now.Date;
        return true;
    }
}
