namespace Application.DTO;
public class PaginationDTO<T>
{
	public T[] Items { get; set; }
	public int Page { get; set; }
	public int Take { get; set; }
	public int Total { get; set; }

	public override string ToString()
	{
		return $"List: {nameof(T)} Page: {Page}, Total: {Total}";
	}
}