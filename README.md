fx-sharp
========

**Previously**
```C#
var name = TelephoneDirectory.LookUpName("+49394965006"); // LookUpName returns string
if (name == null) {
  name = "John Doe";
}
```
**With fx-sharp**
```C#                                           
var name = TelephoneDirectory
            .LookUpName("+49394965006") // LookUpName returns Maybe[string]
            .GetOrElse("John Doe");
```                                        
