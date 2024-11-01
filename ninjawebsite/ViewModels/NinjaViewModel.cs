using ninjawebsite.Models;

public class NinjaViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Gold { get; set; }

    public Equipment HeadEquipment { get; set; }
    public Equipment ChestEquipment { get; set; }
    public Equipment HandEquipment { get; set; }
    public Equipment FeetEquipment { get; set; }
    public Equipment RingEquipment { get; set; }
    public Equipment NecklaceEquipment { get; set; }
}
