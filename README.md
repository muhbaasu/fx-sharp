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

**Key benefits**

* Often no explicit mutable state necessary
* Allows chaining/composing of possibly failing operations
* The Maybe type carries the effect (possible failure) in the signature
* The compiler forces error checking
