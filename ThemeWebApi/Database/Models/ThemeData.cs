namespace ThemeWebApi.Database.Models;

public class ThemeData
{
	public string Name { get; set; }
	public string Description { get; set; }

	public virtual DataTableCellColors DataTableCellColors { get; set; }
	public virtual DropdownButtonColors DropdownButtonColors { get; set; }
	public virtual FloatingBoxColors FloatingBoxColors { get; set; }

	public string DefaultTextColor { get; set; }
	public string DefaultNavMenuTextColor { get; set; }
	public string DefaultNavMenuBackgroundColor { get; set; }
	public string DefaultAppBackGroundColor { get; set; }
	public string ActiveColor { get; set; }
	public string EditColor { get; set; }
	public string CancelColor { get; set; }
}