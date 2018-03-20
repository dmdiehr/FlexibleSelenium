# FlexibleSelenium

### Description

FlexibleSelenium is collection of classes designed to make the .NET bindings for Selenium Webdriver easier to use. FlexibleSelenium adheres to .NET Standard 2.0.

### Installation

FlexibleSelenium can be installed via the nuget package manager. (See https://www.nuget.org/packages/Flexible.Selenium)


### Usage

Currently, FlexibleSelenium is divided into four namespaces. FlexibleSelenium.ByExtensions, FlexibleSelenium.PageElements, FlexibleSelenium.IWebElementExtensions, and FlexibleSelenium.StaticDriver

#### ByExtensions

* The FlexibleSelenium.ByExtensions namespace holds several classes that extend the Selenium.By class. In keeping with the behavior in Selenium Webdriver these classes cannot be directly instantiated and instead are made available via static methods on the ByExtension class.
```csharp
IWebElement foo = Driver.FindElement(ByExtension.Text("bar"));
```
#### PageElements

* The FlexibleSelenium.PageElements namespace holds the PageElement class and the IconLinkElement class which extends from it. 

* PageElement is a wrapper class for IWebElements that makes them easier to work with. To begin wtih, they can be instantiated without currently being available via the WebDriver and thus can retain information, be passed as arguments for future use, and can invoke methods without throwing null exceptions. Instead of representing an element in the dom, they are an object that knows how to find their element in the dom. This allows for built in waiting and exception catching. 

* PageElement implements the IWebElement interface and has multiple constructors. Thus PageElements can be used to completely replace any references to IWebElements. Some IWebElement extension methods may need to be called on PageElement.BaseElement to work properly.

* The PageElement class can be extended to represent element patterns that appear frequently within your automation framework or are otherwise complicated enough that formally establishing the relationships within those patterns makes your automation and testing more understandable and easier to perform.

* For example, you may have a widget, say a filter, that appears in a multitude of locations in the system under test. This filter could have several input fields, checkboxes, radio buttons, and submit and clear buttons. These type of elements will frequently require different locators depending on what page they are on but will usually have the same relationship with a specific base element (in this case maybe a form element). In this case, you can extend the PageElement class to create a FilterElement class that holds, and knows how to locate the various child PageElements. Thus making all the elements in the widget accessible by instantiating one instance (requiring only a single locator) of FilterElement on any page that it may be located.

* The IconLinkElement class is included mainly as an example of how to usefully extend the PageElement class. In this particular case, there was found a situation where the  PageElement.IsPresent method's default behavior was found to be inaccurate. For certain elements, the IWebElement.Displayed value will be false when they are in reality present. (In this case, where a link is being hidden and an icon is shown with css instead.) Thus, this class overrrides the IsPresent method to instead of returning the IWebElement.Displayed value, it returns whether the element is both visible and has a non-emmpty size.

#### IWebElementExtensions

* TBD

#### StaticDriver

* TBD
