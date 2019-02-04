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

                <behavior:EventToCommandBehavior EventName="ItemSelected" Command="{Binding OutputAgeCommand}" />

            </ListView.Behaviors>

        </ListView>

### Constants

to make our solution more maintainable we should use all constants as global variables so Constants assembly will hold all keys in class such as _API keys_ 

### CustomControls
every Xamarin Forms application need a custom control and renderers to for custom UI in Custom Control assembly will have all responsible to hold custom controls, for now, we only have a _Grid View_ 

#### Grid View

grid view is a control simply make grid layout bindable object with data source and item template
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
any application noew must have at least on popup, here we use Popup Page Plugin for Xamarin Forms, and to wire popup to view model we use base popup class and call on lifetime virtul methods.

### Services
in white mvvm we implement many service such as Navigation, Dialog or Device Utilities
#### Api
till now we use refit plugin to call api, because make Http Client call more simple and less code
Refit depend on interface contain method without body and attributes over methods 

#### Dialog
Dialog service is an assembly has responsablty of any regular popup aslo loading indicator

##### Methods
- ShowAlertAsync(string message, string title, string buttonLabel): this void method use to show dialog to user with meesage, title and one button
- ShowConfirmMessageAsync(string message, string title = "Confirm", string cancelText = "Cancel",string okText = "Ok"): this bool method that return whatever user select ok or cancel with option to change ok and cancel labels
- ShowLoading() : to show loading indicator
- HideLoading() : to Hide Loading indicator
 
#### Navigation 
in navigation in white mvvm we go with the of most of mvvm framework and Xamarin recommendation way which depend on navigate view model instaed of pages to reach to an totally separated code
the aim to make view only response for desgin and make navigation system in view model

##### Methods
- NavigateToAsync<TViewModel>(object parameter = null): generic async method to push page in navigation stack or start app with navgation stack take one optinal paramter which will send to view mdoel type you inserted
- NavigateModalToAsync<TViewModel>(object parameter = null): generic async method to push page as modal or start app with this page take one optinal paramter which will send to view mdoel type you inserted
- InitializeAsync() : method to Initialize Navigation service in app class where we can write how we will begin or navigation 
- NavigateToTabbedAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null): async method to naviagte to tabbed page which take list of page container class and tabbed page as optional parameter
- NavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false) : async method to navigate to master detail page with master and detail page as paramter aslo if you want to keep navigation bar or not as optinal paramter also if you want to use custom master detial page
- ChangeDetailPage(PageContainer pageContainer) : mehtod to change details page with another on take one paramter page container
- AddPageToTabbedPage(PageContainer pageContainer) : method to add tabbed page in run time take one paramter page container

##### Classes
###### PageContainer 
 class use as contianer in navigation to increse options when use navigation 
*Properties*
- Parameter: param to pass to view model 
- ViewModel: viewmodel that wired with page
- IsNavigationPage: if we want to make this page start as navigation page
- PageName: name of page we will navigate, note that name will use in tabbed page

#### Utilities
assembly which contain all Xamarin Essentail services with interface approach and mocks for unit test

### Transitions
Transition is an layer between models and api service to separate thoese layer and make change more clear and has not side effect in code

#### Classes
##### BaseTransitional
this class has one method to convert from transitionl object to model object, this method is virtual which we can override and map our transitional to model

### Utilities

#### NotifiedObject
Notified Object is an base object that implement INotifyPropertyChanged 

#### ObservableRangeCollection
as ObersavleCollection is recommended to use in list of object then list but it do not have add range method we add this method to our solution in this class

#### TaskCommand
in Async programming try to avoid void with async methods and all of command is void action so here we implement a command that excute task not void method

#### TransitionalList
this an list that help Transition layer to do ToModel method in list

### Validations
Any app that accepts input from users should ensure that the input is valid. An app could, for example, check for input that contains only characters in a particular range, is of a certain length, or matches a particular format. Without validation, a user can supply data that causes the app to fail. Validation enforces business rules, and prevents an attacker from injecting malicious data.

In the context of the Model-View-ViewModel (MVVM) pattern, a view model or model will often be required to perform data validation and signal any validation errors to the view so that the user can correct them. The eShopOnContainers mobile app performs synchronous client-side validation of view model properties and notifies the user of any validation errors by highlighting the control that contains the invalid data, and by displaying error messages that inform the user of why the data is invalid.

#### Specifying Validation Rules
Validation rules are specified by creating a class that derives from the IValidationRule<T> interface,This interface specifies that a validation rule class must provide a boolean Check method that is used to perform the required validation, and a ValidationMessage property whose value is the validation error message that will be displayed if validation fails.

#### Adding Validation Rules to a Property
view model properties that require validation are declared to be of type ValidatableObject<T>, where T is the type of the data to be validated
For validation to occur, validation rules must be added to the Validations collection of each ValidatableObject<T> instance
#### Triggering Validation
Validation can be triggered manually for a view model property. For example, this occurs in any mobile app when the user taps the Login button on the LoginView we call Validate method which performs validation of the username and password entered by the user on the LoginView, by invoking the Validate method on each ValidatableObject<T> instance.
This method clears the Errors collection, and then retrieves any validation rules that were added to the object's Validations collection. The Check method for each retrieved validation rule is executed, and the ValidationMessage property value for any validation rule that fails to validate the data is added to the Errors collection of the ValidatableObject<T> instance. Finally, the IsValid property is set, and its value is returned to the calling method, indicating whether validation succeeded or failed.

### ViewModel
in view model assembly we used base viewmodel class and view model locator with unity container

#### Base ViewModel
in base view model we wire page and popup event with page by use virtual methods and call it in base page aslo we add an initilaizer method which run once in navigation between pages
and we also call dialog and navigation service that we be with us in every class drivid this class

#### ViewModel Locator 
if we want to use dependancy injection we must select container, here we choose Unity with some additional methods 

