fx-sharp
========

The name fx-sharp stands for functional sharp. Goal of the project is to bring advanced functional concepts, such as the [Maybe(Option) type](http://en.wikipedia.org/wiki/Option_type) or the Either type, to C#. 


Maybe
-------

**Key benefits**

* Often no explicit mutable state necessary
* Allows chaining/composing of possibly failing operations
* The Maybe type carries the effect (possible failure) in the signature
* The compiler forces error checking, no chance for a NullReference exception during runtime
* [Convenience methods](http://muhbaasu.github.io/fx-sharp/struct_fx_sharp_1_1_maybe_3_01_t_01_4.html) to handle possible failure
* Often avoids the need to check for failure at all. If you stick to the convention that methods that may fail always return `Maybe<T>`, while other methods which may always return a valid value do not need to be checked.
E.g. `buddyStorage.GetBuddies()` could at minimum return an empty list.

**Chaining of operations which may fail**

This example demonstrates a case, where multiple sequential operations are dependent on the result of a prior operation.

_Previously_
```C#
Caller caller = null;
var name = TelephoneDirectory.LookUpName("+49394965006"); // LookUpName returns null when not found
if (name != null) {
  var photo = SocialNetwork.LookUpPhoto(name); // LookUpPhoto returns null when not found
  if (phote != null) {
    caller = new Caller(name, photo);
  }
}
```

_With fx-sharp_
```C#                                           
var caller = from name in TelephoneDirectory.LookUpName("+49394965006") // LookUpName returns Maybe[string]
             from photo in SocialNetwork.LookUpPhoto(name)              // LookUpPhoto returns Maybe[Image]
             select new Caller(name, photo);
```

**Handle failure and success**

"Pattern match" the result of a method that returns a `Maybe`.

_Previously_
```C#
var name = TelephoneDirectory.LookUpName("+49394965006") // LookUpName returns null when not found
if (name != null} {
  view.DisplayName(name.ToUpper())
} else {
  view.DisplayName("Unknown caller"))
}

```

_With fx-sharp_
```C#
TelephoneDirectory
    .LookUpName("+49394965006") // LookUpName returns Maybe[string]
    .Match_(
        just: name => view.DisplayName(name.ToUpper()),
        nothing: () => view.DisplayName("Unknown caller"));
```

**Default Value**

Provide a default value, if a function returns `Nothing`.

_Previously_
```C#
var name = TelephoneDirectory.LookUpName("+49394965006") ?? "John Doe"; // LookUpName returns null
```

_With fx-sharp_
```C#                                           
var name = TelephoneDirectory
            .LookUpName("+49394965006") // LookUpName returns Maybe[string]
            .GetOrElse("John Doe");
```


