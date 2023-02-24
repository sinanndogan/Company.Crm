namespace Company.Crm.Web.Mvc.Models;

public class BootstrapModel
{
	public string? Id { get; set; }
	public string? AreaLabeledId { get; set; }
	public ModalSize Size { get; set; }
	public string ModalSizeClass
	{
		get
		{
			switch (this.Size)
			{
				case ModalSize.Small:
					return "modal-sm";
				case ModalSize.Large:
					return "modal-lg";
				case ModalSize.Medium:
				default:
					return "";
			}
		}
	}
}

public enum ModalSize
{
	Small,
	Large,
	Medium
}