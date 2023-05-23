using System.Net;
using System.Text.RegularExpressions;
using TestTaskParcer.Data.Models;
using TestTaskParcer.Service.Interfaces;

namespace TestTaskParcer;

public class Parcer
{
    private readonly WebClient _client;
    private readonly IGuideService _guide;

    public Parcer(IGuideService guide)
    {
        _client = new();
        _guide = guide;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public async void Start(string url)
    {
        string htmlCode = _client.DownloadString(url);

        var table = GetCarTable(htmlCode);

        foreach (var carCode in table)
        {
            var name = GetCarName(carCode);
            var dates = GetCarDate(carCode);
            var codes = GetCarCodeModel(carCode);

            var ids = await GetCarId(carCode);

            for (int i = 0; i < codes.Count; i++)
            {
                await _guide.AddCar(ids.Item1[i], name, dates[i], codes[i], ids.Item2[i]);
            }
        }
    }

    public List<string> GetCarTable(string htmlCode)
    {
        var regex = "<div class=\"List\">(.*?)</div></div></div>";
        var matches = Regex.Matches(htmlCode, regex);
        return matches.Select(s => s.ToString()).ToList();
    }

    public string GetCarName(string htmlCode)
    {
        // Отримання назв автівок
        var regex = "<div class=\"Header\"><div class=\"name\">(.*?)</div>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("e\">")[1]
                .Split("<")[0];
    }

    public List<string> GetCarDate(string htmlCode)
    {
        // Отримання Дати випуску
        var regex = "<div class=\"dateRange\">(.*?)</div>";
        var matches = Regex.Matches(htmlCode, regex);

        return matches.Select(s => s
                .ToString()
                .Split("\">")[1]
                .Split("<")[0]
                .Replace("&nbsp;", " "))
            .ToList();
    }

    public List<string> GetCarCodeModel(string htmlCode)
    {
        // Отримання коду моделі
        var regex = "<div class=\"modelCode\">(.*?)</div>";
        var matches = Regex.Matches(htmlCode, regex);

        return matches.Select(s => s
                .ToString()
                .Split("\">")[1]
                .Split("<")[0])
            .ToList();

    }

    public async Task<(List<int>, List<List<CarComplectation>>)> GetCarId(string htmlCode)
    {
        // Отримання айді автівки
        var regex = "<div class=\"List\"><div class=\"id\"><a href=\"(.*?)\" title target>(.*?)</a></div>";
        var matches = Regex.Matches(htmlCode, regex);
        var result = matches.Select(s => s
                .ToString()
                .Split("target>")[1]
                .Split("<")[0])
            .Select(s => int.Parse(s))
            .ToList();
        var complectations = new List<List<CarComplectation>>();
        for (int i = 0; i < matches.Count; i++)
        {
            var link = "https://www.ilcats.ru" + matches[i]
                .ToString()
                .Split("href=\"")[1]
                .Split("\"")[0];

            complectations.Add(await GetComplectation(link, result[i]));
        }

        return (result, complectations);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public async Task<List<CarComplectation>> GetComplectation(string url, int id)
    {
        string htmlCode = _client.DownloadString(url);

        var complectationList = new List<CarComplectation>();
        var complectations = GetComplectationRows(ChooseTable(htmlCode));
        foreach (var row in complectations)
        {
            var period = GetPeriod(row);
            var engine = GetEngine(row);
            var body = GetBody(row);
            var grade = GetGrade(row);
            var atm = GetAtm(row);
            var gearShiftType = GetGearShiftType(row);
            var position = GetCarDriverPosition(row);
            var doors = GetDoors(row);
            var destination = GetDestination(row);
            var codeModel = await GetModelCode(row);

            complectationList.Add(await _guide.AddComplectation(codeModel.Item2, period, engine, body, grade, atm, gearShiftType, position, doors, destination, codeModel.Item1));
        }
        return complectationList;
    }

    public string ChooseTable(string htmlCode)
    {
        var regex = "<tbody>(.*?)</tbody>";
        var match = Regex.Matches(htmlCode, regex).Select(s => s.ToString()).ToList();
        return match[0];
    }

    public List<string> GetComplectationRows(string htmlCode)
    {
        var regex = "</tr><tr><td><div class(.*?)</td></tr>";
        var matches = Regex.Matches(htmlCode, regex);

        return matches.Select(s => s.ToString())
            .ToList();
    }

    public string GetDestination(string htmlCode)
    {
        // Отримання Destination
        var regex = "<td><div class=\"09\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetDoors(string htmlCode)
    {
        // Отримання No of Doors
        var regex = "<td><div class=\"08\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetCarDriverPosition(string htmlCode)
    {
        // Отримання Driver'sPosition
        var regex = "<td><div class=\"07\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetGearShiftType(string htmlCode)
    {
        // Отримання GearShiftType
        var regex = "<td><div class=\"06\">(.*?)</div></td>";
        var matches = Regex.Matches(htmlCode, regex);
        return matches.ToString()!
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetAtm(string htmlCode)
    {
        // Отримання Atm
        var regex = "<td><div class=\"05\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetGrade(string htmlCode)
    {
        // Отримання Grade
        var regex = "<td><div class=\"04\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetBody(string htmlCode)
    {
        // Отримання Тіла
        var regex = "<td><div class=\"03\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetPeriod(string htmlCode)
    {
        // Отримання періоду виготовлення
        var regex = "<td><div class=\"dateRange\">(.*?)</div></td>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0]
                .Replace("&nbsp;", " ");
    }

    public string GetEngine(string htmlCode)
    {
        // Отримання Двигуна
        var regex = "<td><div class=\"01\">(.*?)</div></td>";
        var matches = Regex.Matches(htmlCode, regex);
        return matches.ToString()!
                .Split("\">")[1]
                .Split("<")[0];
    }

    public async Task<(List<PartGroup>, string)> GetModelCode(string htmlCode)
    {
        // Отримання коду моделі кузова
        var regex = "<div class=\"modelCode\"><a href=\"(.*?)\" title target>(.*?)</a></div>";
        var matches = Regex.Match(htmlCode, regex);
        var result = matches.ToString()
                .Split("target>")[1]
                .Split("<")[0];


        var link = "https://www.ilcats.ru" + matches
            .ToString()
            .Split("href=\"")[1]
            .Split("\"")[0];

        var groups = await GetGroups(link);

        return (groups, result);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public async Task<List<PartGroup>> GetGroups(string url)
    {
        var htmlCode = _client.DownloadString(url);
        var groupsCode = GetGroupBlock(htmlCode);

        var groups = new List<PartGroup>();
        foreach (var group in groupsCode)
        {
            groups.Add(await GetGroup(group));
        }

        return groups;
    }

    public async Task<PartGroup> GetGroup(string htmlCode)
    {
        // Отримання груп запчастин
        var regex = "<div class=\"name\"><a href=\"(.*?)\" title target>(.*?)</a></div>";
        var matches = Regex.Match(htmlCode, regex);
        var name = matches.ToString()
                .Split("target>")[1]
                .Split("<")[0];


        var link = "https://www.ilcats.ru" + matches
            .ToString()!
            .Split("href=\"")[1]
            .Split("\"")[0];

        var types = await GetTypes(link);

        return await _guide.AddParentGroup(name, types);
    }

    public List<string> GetGroupBlock(string htmlCode)
    {
        var regex = "<div class=\"List\"><div class=\"name\"><a href=\"(.*?)\" title target>(.*?)</a></div></div>";
        var matches = Regex.Matches(htmlCode, regex);

        return matches.Select(s => s
                .ToString())
            .ToList();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public async Task<List<SubGroup>> GetTypes(string url)
    {
        var htmlCode = _client.DownloadString(url);
        var types = GetTypeBlock(htmlCode);
        var groups = new List<SubGroup>();

        foreach (var type in types)
        {
            groups.Add(await GetType(type));
        }

        return groups;
    }

    public List<string> GetTypeBlock(string htmlCode)
    {
        var regex = "<div class=\"List\"><div class=\"image\">(.*?)</a></div>";
        var matches = Regex.Matches(htmlCode, regex);

        return matches.Select(s => s
                .ToString())
            .ToList();
    }

    public async Task<SubGroup> GetType(string htmlCode)
    {
        // Отримання типу запчастини
        var regex = "<div class=\"name\"><a href=\"(.*?)\" title target>(.*?)</a></div>";
        var matches = Regex.Match(htmlCode, regex);
        var name = matches.ToString()
                .Split("target>")[1]
                .Split("<")[0];

        var link = "https://www.ilcats.ru" + matches
            .ToString()!
            .Split("href=\"")[1]
            .Split("\"")[0];

        var details = await GetSchema(link);
        return await _guide.AddSubGroup(name, details.Item2, details.Item1);
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public async Task<(List<Detail>, string)> GetSchema(string url)
    {
        var htmlCode = _client.DownloadString(url);

        var trees = GetTreeName(htmlCode);
        var codeTree = new List<string>(trees.Count);
        var tree = new List<string>(trees.Count);
        for (int i = 0; i < trees.Count; i++)
        {
            var item = trees[i].Split("&nbsp;");
            codeTree[i] = item[0];
            tree[i] = item[1];
        }

        var details = new List<Detail>();
        for (int i = 0; i < codeTree.Count; i++)
        {
            var code = GetDetailRows(htmlCode, codeTree[i]);
            var info = new List<Infos>();
            foreach (var row in code)
            {
                var partId = GetPartId(row);
                var partCount = GetPartCount(row);
                var partPeriod = GetPartPeriod(row);
                var partInfo = GetPartInfo(row);

                info.Add(new Infos
                {
                    Count = partCount,
                    Date = partPeriod,
                    Info = partInfo,
                    Id = partId
                }); // створення інфо та додавання до лісту
            }

            details.Add(await _guide.AddDetail(codeTree[i], tree[i], info));
        }
        var photo = GetImage(htmlCode);

        return (details, photo); // створення деталі та повернення деталі
    }

    public List<string> GetDetailRows(string htmlCode, string codeTree)
    {
        var regex = $"<tr class=\"(.*?) data-id=\"{codeTree}\"><td>(.*?)</td></tr>";
        var matches = Regex.Matches(htmlCode, regex);
        return matches.Select(s => s
                .ToString())
            .ToList();
    }

    public List<string> GetTreeName(string htmlCode)
    {
        // назву та код назви
        var regex = "<th colspan=\"(.*?)\">(.*?)</th>";
        var matches = Regex.Matches(htmlCode, regex);
        return matches.Select(s => s
                .ToString()
                .Split("\">")[1]
                .Split("<")[0])
            .ToList();
    }

    public string GetPartInfo(string htmlCode)
    {
        // Отримання применяємость info
        var regex = "<div class=\"usage\">(.*?)</div>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetPartPeriod(string htmlCode)
    {
        // Отримання періоду виготовлення date
        var regex = "<div class=\"dateRange\">(.*?)</div>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0]
                .Replace("&nbsp;", " ");
    }

    public string GetPartCount(string htmlCode)
    {
        // Отримання кількості запчастин count
        var regex = "<div class=\"count\">(.*?)</div>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("\">")[1]
                .Split("<")[0];
    }

    public string GetPartId(string htmlCode)
    {
        // Отримання айді запчастини code
        var regex = "<div class=\"number\"><a href=\"(.*?)\" target=\"_blank\">(.*?)</a></div>";
        var matches = Regex.Match(htmlCode, regex);
        return matches.ToString()
                .Split("k\">")[1]
                .Split("<")[0];
    }

    public string GetImage(string code)
    {
        var caca = _client.DownloadString(code);
        var regex = "<img src=\"//images(.*?)\"";
        var match = Regex.Match(caca, regex);
        var kaka = match.ToString();
        var url = match.ToString().Split("src=\"")[1].Split("\"")[0];

        while (true)
        {
            var newName = Guid.NewGuid().ToString() + ".png";
            if (!Directory.Exists(newName))
            {
                _client.DownloadFileAsync(new Uri("https:" + url), newName);
                return newName;
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
