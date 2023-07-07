namespace ToDoListClient.ViewModels
{
    public class ShowContextMenuBehavior : Behavior<ImageButton>
    {
        protected override void OnAttachedTo(ImageButton bindable)
        {
            bindable.Clicked += OnClicked;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ImageButton bindable)
        {
            bindable.Clicked -= OnClicked;
            base.OnDetachingFrom(bindable);
        }

        private void OnClicked(object sender, EventArgs e)
        {
        }
    }
}