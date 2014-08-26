fx-sharp
========

The name fx-sharp stands for functional sharp. Goal of the project is to bring advanced functional concepts, such as the [Maybe(Option) type](http://en.wikipedia.org/wiki/Option_type) or the Either type, to C#. 


Maybe
-------

**Key benefits**

* Often no explicit mutable state necessary
* Allows chaining/composing of possibly failing operations
* The Maybe type carries the effect (possible failure) in the signature
* The compiler forces error checking

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
