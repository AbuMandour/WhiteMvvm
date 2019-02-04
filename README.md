# WhiteMvvm
White Solution is an MVVM framework for Xamarin Forms Solutions to make the code as white "Clear, simple and powerful"

## Get Start
you can download the template from here
then make sure that solution build successfully

 
## Documentation

White MVVM build to be more clear to modify and here we will explain how every part of solution work


### Assets

Assets place where we can put or CSS files if we decided to use CSS to style our application

### Behaviors
Behaviours enable you to implement code that you would normally have to write as code-behind because it directly interacts with the API of the control in such a way that it can be concisely attached to the control.
In White solution, we use behaviours to associate commands with controls that were not designed to interact with commands,
the following code is an example in C#
 

            
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

                <behavior:EventToCommandBehavior EventName="ItemSelected" Command="{Binding OutputAgeCommand}" />

            </ListView.Behaviors>

        </ListView>

### Constants

to make our solution more maintainable we should use all constants as global variables so Constants assembly will hold all keys in class such as _API keys_ 

### CustomControls
every Xamarin Forms application need a custom control and renderers to for custom UI in Custom Control assembly will have all responsible to hold custom controls, for now, we only have a _Grid View_ 

#### Grid View

grid view is a control simply make grid layout bindable object with the data source and item template
##### Properties

- **ItemsSource**: _IEnumerable_ property to set data source for every item template
- **SelectedItem**: _Object_ the item which selected on tapped
-**SelectedCommand**: _ICommand_ fires when the user tapped in item
- **ItemMargin**: _Thikness_  margin for each item
- **ItemTemplate**: _DataTemplate_ for each item 

##### How to use
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

### Models
model is an essential member in most of the MVVM architecture, in White MVVM we create a Base Model class which inherited from Notified Object and have an ID property which will be useful to make every object of model recognizable.

### Popup
any application new must have at least on a popup, here we use Popup Page Plugin for Xamarin Forms, and to wire popup to view model we use base popup class and call on lifetime virtual methods.

### Services
in white MVVM we implement many services such as Navigation, Dialog or Device Utilities

#### API
till now we use refit plugin to call API because make Http Client call more simple and less code
Refit depend on interface contain method without body and attributes over methods 

#### Dialog
Dialog service is an assembly has the responsibility of any regular popup also loading indicator we depend on UserDialogs plugin yo implement this all dialog

#### Navigation 
navigation in white MVVM we go with the of most of the MVVM framework and Xamarin recommendation way which depend on navigating view model instead of pages to reach a totally separated code
the aim to make view only responsible for design and make navigation system in view model but here naming convention is very imported as all pages should end with "view" and all view models should end with ViewModel and use master details page and tabbed page we must use page container class which has options and navigate from view model

#### Utilities
the assembly which contain all Xamarin Essential services with interface approach and mocks for unit test

### Transitions
The transition is a layer between models and API service to separate those layer and make change more clear and has no side effect in code

### Utilities

#### NotifiedObject
Notified Object is an base object that implement INotifyPropertyChanged 

#### ObservableRangeCollection
as ObersavleCollection is recommended to use in the list of an object then list but it does not have add range method we add this method to our solution in this class

#### Task Command
in Async programming try to avoid void with async methods and all of the command is void action so here we implement a command that executes the task, not a void method

#### TransitionalList
this a list that help Transition layer to do ToModel method in the list

### Validations
Any app that accepts input from users should ensure that the input is valid. An app could, for example, check for input that contains only characters in a particular range, is of a certain length, or matches a particular format. Without validation, a user can supply data that causes the app to fail. Validation enforces business rules and prevents an attacker from injecting malicious data.

In the context of the Model-View-ViewModel (MVVM) pattern, a view model or model will often be required to perform data validation and signal any validation errors to the view so that the user can correct them. The eShopOnContainers mobile app performs synchronous client-side validation of view model properties and notifies the user of any validation errors by highlighting the control that contains the invalid data, and by displaying error messages that inform the user of why the data is invalid.

#### Specifying Validation Rules
Validation rules are specified by creating a class that derives from the IValidationRule<T> interface, This interface specifies that a validation rule class must provide a boolean Check method that is used to perform the required validation and a ValidationMessage property whose value is the validation error message that will be displayed if validation fails.

#### Adding Validation Rules to a Property
view model properties that require validation are declared to be of type ValidatableObject<T>, where T is the type of the data to be validated
For validation to occur, validation rules must be added to the Validations collection of each ValidatableObject<T> instance
#### Triggering Validation
Validation can be triggered manually for a view model property. For example, this occurs in any mobile app when the user taps the Login button on the LoginView we call Validate method which performs validation of the username and password entered by the user on the LoginView, by invoking the Validate method on each ValidatableObject<T> instance.
This method clears the Errors collection and then retrieves any validation rules that were added to the object's Validations collection. The Check method for each retrieved validation rule is executed, and the ValidationMessage property value for any validation rule that fails to validate the data is added to the Errors collection of the ValidatableObject<T> instance. Finally, the IsValid property is set, and its value is returned to the calling method, indicating whether validation succeeded or failed.

### ViewModel
the view model is a member of MVVM architecture, in our view model assembly we used base ViewModel class and view model locator with unity container

#### Base ViewModel
in base view model we wire page and popup event with the page by using virtual methods and call it in base page also we add an initializer method which run once in navigation between pages
and we also call dialog and navigation service that we be with us in every class inherited this class

#### ViewModel Locator 
if we want to use dependency injection we must select a container, here we choose Unity with some additional methods, the main concept in locator to auto-wire view with view model and that must make the naming convention more important and use mock service and update them when the unit test run 
