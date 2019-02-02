# WhiteMvvm
##Behaviors
Behaviors enable you to implement code that you would normally have to write as code-behind, because it directly interacts with the API of the control in such a way that it can be concisely attached to the control.
In White solution we use behaviors to associate commands with controls that were not designed to interact with commands,
following code is example in C#
 

            
            var listView = new ListView();
            listView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "People");

            listView.ItemTemplate = new DataTemplate(() =>

            {

                var textCell = new TextCell();

                textCell.SetBinding(TextCell.TextProperty, "Name");

                return textCell;

            });

            listView.Behaviors.Add(new EventToCommandBehavior

            {

                EventName = "ItemSelected",

                Command = ((HomePageViewModel)BindingContext).OutputAgeCommand,

                Converter = new SelectedItemEventArgsToSelectedItemConverter()

            });


and this for XAML

        <ListView ItemsSource="{Binding People}">

            <ListView.ItemTemplate>

                <DataTemplate>

                    <TextCell Text="{Binding Name}" />

                </DataTemplate>

            </ListView.ItemTemplate>

            <ListView.Behaviors>

                <local:EventToCommandBehavior EventName="ItemSelected" Command="{Binding OutputAgeCommand}" Converter="{StaticResource SelectedItemConverter}" />

            </ListView.Behaviors>

        </ListView>

