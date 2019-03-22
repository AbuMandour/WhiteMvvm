# WhiteMvvm
White Solution Portable [![Build status](https://abumandour.visualstudio.com/WhiteMvvm/_apis/build/status/WhiteMvvm-CI)](https://abumandour.visualstudio.com/WhiteMvvm/_build/latest?definitionId=8)

White Solution Android [![Build status](https://abumandour.visualstudio.com/WhiteMvvm/_apis/build/status/WhiteMvvmDroid-CI)](https://abumandour.visualstudio.com/WhiteMvvm/_build/latest?definitionId=12)

WhiteSolution iOS [![Build status](https://abumandour.visualstudio.com/WhiteMvvm/_apis/build/status/WhiteMvvmiOS-CI)](https://abumandour.visualstudio.com/WhiteMvvm/_build/latest?definitionId=13)

White Solution UnitTest [![Build status](https://abumandour.visualstudio.com/WhiteMvvm/_apis/build/status/WhiteMvvmUnitTest-CI)](https://abumandour.visualstudio.com/WhiteMvvm/_build/latest?definitionId=14)

White Solution is an MVVM framework for Xamarin Forms Solutions to make the code as white "Clear, simple and powerful"

## GetStarted
you can download the nuget for portable project from here https://www.nuget.org/packages/WhiteMvvm/

and download the nuget for Android project from here https://www.nuget.org/packages/WhiteMvvm.Droid/

and download the nuget for iOS project from here https://www.nuget.org/packages/WhiteMvvm.iOS/

and download the nuget for UnitTest project (***if you have one***) from here https://www.nuget.org/packages/WhiteMvvm.UnitTest/


then you should call this method in each platform

***Android***: WhiteMvvm.Droid.PlatformDorid.Init(this,savedInstanceState);

***iOS***: WhiteMvvm.Droid.PlatformDorid.Init();

Now you ready to use white mvvm, welcome :)

 
## Documentation

White MVVM build to be more clear to modify and here we will explain how every part of solution work


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

                Command = ((HomePageViewModel)BindingContext).SelectProductCommand,

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

                <behavior:EventToCommandBehavior EventName="ItemSelected" Command="{Binding SelectProductCommand}" />

            </ListView.Behaviors>

        </ListView>


### CustomControls
every Xamarin Forms application need a custom control and renderers to for custom UI in Custom Control assembly will have all responsible to hold custom controls, for now, we only have a _Content Presenter_ 

#### Content Presenter

Content Presenter is a control simply allow you to create a data template for single record

### Bases


**BaseModel**: in White MVVM we create a Base Model class which inherited from Notified Object and have an ID property which will be useful to make every object of model recognizable.

**BaseTransitional**: The transition is a layer between models and API service to separate layers and make change more clear and has no side effect in other modules, any class that inherited from Base transitional class has option to override ToModel method where you can mapping from api to model

**BaseViewModel**: in base view model we wire page and popup events with the page by using virtual methods also we add an initializer method which run once when app navigate from viewmodel to viewmodel 
and we also initialize dialog and navigation service that we be with us in every class inherited this class

**BaseViewModelLocator**: if we want to use dependency injection we must select a container, here we choose Unity with some additional methods, the main concept in our locator is auto-wire view with view model and use mock service and update them during the unit test run

### Services
in white MVVM we implement many services such as Navigation, Dialog or Device Utilities

#### Dialog
Dialog service is an assembly has the responsibility of any regular popup also loading indicator we depend on UserDialogs plugin yo implement this all dialog

             await DialogService.ShowAlertAsync("No Internet Connection", "Internet", "Ok");

        
#### Navigation 
navigation in white MVVM we go with the of most of the MVVM framework and Xamarin recommendation way which depend on navigating view model instead of pages to reach a totally separated code
the aim to make view only responsible for design and make navigation system in view model but here naming convention is very imported as all pages should end with "view" and all view models should end with ViewModel and use master details page and tabbed page we must use page container class which has options and navigate from view model

##### GetStarted

first we should change some code in app class, like this:


             public partial class App : WhiteApplication

 _to_

             public partial class App : WhiteApplication
        

then select our launch page with SetHomePage method: 

             SetHomePage<HomeViewModel>

        
now we can use all navigation methods to navigate between view models

#### DeviceUtilities
the assembly which contain all Xamarin Essential services with interface approach and mocks for unit test

### Utilities

#### NotifiedObject
Notified Object is an base object that implement INotifyPropertyChanged 

#### ObservableRangeCollection
as ObersavleCollection is recommended to use in the list of an object then list but it does not have add range method we add this method to our solution in this class

#### TaskCommand
in Async programming try to avoid void with async methods and all of the command is void action so here we implement a command that executes the task, not a void method

#### TransitionalList
this a list that help Transition layer to do ToModel method in the list


#### Notes:
- you must make syre that we follow our naming conventions in naming of view and view model
1- Views: Views.{Name}View
2- View Models: ViewModels.{Name}ViewModel

- we only support iOS and Android

- we take validation module from *Enterprise Application Patterns eBook* as we found that the best solution for as

