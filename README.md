# WhiteMvvm
White Solution is an MVVM framework for Xamarin Forms Solutions to make the code as white "Clear, simple and powerful"
[[_TOC_]]

##Get Start
you can download the template from here
then make sure that solution build successfully

 
##Documentation

White MVVM build to be more clear to modify and here we will explain how every part of solution work


###Assets

Assets place where we can put or CSS files if we decided to use CSS to style our application

###Behaviors
Behaviours enable you to implement code that you would normally have to write as code-behind because it directly interacts with the API of the control in such a way that it can be concisely attached to the control.
In White solution, we use behaviours to associate commands with controls that were not designed to interact with commands,
following code is an example in C#
 

            
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

###Constants

to make our solution more maintainable we should use all constants as global variables so Constants assembly will hold all keys in class such as _API keys_ 

###CustomControls
every Xamarin Forms application need a custom control and renderers to for custom UI in Custom Control assembly will have all responsible to hold custom controls, for now, we only have a _Grid View_ 

####Grid View

grid view is a control simply make grid layout bindable object with data source and item template
#####Properties

- **ItemsSource**: _IEnumerable_ property to set data source for every item template
- **SelectedItem**: _Object_ the item which selected on tapped
-**SelectedCommand**: _ICommand_ fires when the user tapped in item
- **ItemMargin**: _Thikness_  margin for each item
- **ItemTemplate**: _DataTemplate_ for each item 

#####How to use
           <customcontrol:GridView AutomationId="productGridView"
                                ItemMargin="5"
                                HorizontalOptions="FillAndExpand"
                                VerticalOptions="FillAndExpand"
                                SelectedCommand="{Binding SelectProductCommand}"
                                ItemsSource="{Binding Products}">
                <customcontrol:GridView.ItemTemplate>
                    <DataTemplate>
                        <Frame WidthRequest="80"
                               HeightRequest="80"
                               IsClippedToBounds="True"
                               CornerRadius="5"
                               BackgroundColor="LightSeaGreen">
                            <StackLayout 
                                 HorizontalOptions="FillAndExpand"
                                 VerticalOptions="FillAndExpand">
                                <Label  HorizontalOptions="Center"
                                        FontSize="18"
                                        Text="{Binding Name}"/>
                                <Label HorizontalOptions="Center"
                                       FontSize="14"
                                       Text="{Binding Price}"/>
                            </StackLayout>
                        </Frame>
                    </DataTemplate>
                </customcontrol:GridView.ItemTemplate>
            </customcontrol:GridView>`

###Models
model is an essential member in most of the MVVM architecture, in White MVVM we create a Base Model class which inherited from Notified Object and have an ID property which will be useful to make every object of model recognizable
 