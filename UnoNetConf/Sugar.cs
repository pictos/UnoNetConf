using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace UnoNetConf;
static class Sugar
{

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T MyCustomVisibility<T>(this T element, Func<bool> propertyBinding, [CallerArgumentExpression("propertyBinding")] string? propertyBindingExpression = null)
		where T : UIElement
	{
		return element.Visibility(propertyBinding, isVisible => isVisible ? Visibility.Visible : Visibility.Collapsed, propertyBindingExpression);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Grid<T>(this T element, Enum row, Enum column)
		where T : UIElement
	{
		return element.Grid(row: Convert.ToInt32(row), column: Convert.ToInt32(column));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Grid<T>(this T element, Enum row, int column)
		where T : UIElement
	{
		return element.Grid(row: Convert.ToInt32(row), column: column);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Vertical<T>(this T element)
	where T : StackPanel =>
		element.Orientation(Orientation.Vertical);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Horizontal<T>(this T element)
		where T : StackPanel =>
		element.Orientation(Orientation.Horizontal);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T HorizontalAlignmentCenter<T>(this T element)
		where T : FrameworkElement =>
		element.HorizontalAlignment(HorizontalAlignment.Center);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T HorizontalAlignmentLeft<T>(this T element)
		where T : FrameworkElement =>
		element.HorizontalAlignment(HorizontalAlignment.Left);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T HorizontalAlignmentRight<T>(this T element)
		where T : FrameworkElement =>
		element.HorizontalAlignment(HorizontalAlignment.Right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T HorizontalAlignmentStretch<T>(this T element)
		where T : FrameworkElement =>
		element.HorizontalAlignment(HorizontalAlignment.Stretch);

	public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull
		=> new(dictionary);
}
