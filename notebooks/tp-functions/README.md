# Functions

During this course we will cover the fundamentals of F# programming language and principles of functional programming.

Functional programming languages have being around for more that 60 years. Some of them are the likes of Lisp, Closure, ML, Haskell, F# and many others.

## Functions

First of all, what is functional programming?
This is not a pradigm, it's programming style. Writing programs the functional way does not mean you cannot write some OOP code as well. You can mix the two. 

Functional programming is simply all about functions. So what is a function? Function is something that takes an input and produces an output.

![Function](../../resources/fp-function-definition.png)

First, let's start with defining a function.

```
f(x): int -> int, x c N
```

Here `f` is the name of the function and `x` is the functiona argument. We have defined, that function takes an argument of type `int`, does some transformation of the value and return another instance of type `int`.

This is how you would declare a function in F#:


```C#
// function definition
type Function = int -> int

// function declaration that squares the number
let sqr: Function = fun (x: int) -> x * x
```

We can narrow this down to:


```C#
let sqr x = x * x
```

The left part is value binding - a declaration and initialization of a value. Part after the equal sign is the value itself. In our case it is the function. Functions in F# are treated as values, and so they are first-class citizens.

## Higher-order Functions

Since functions are values, it means that we can pass function into other functions as parameters or return as values. Those type of functions are called `Higher-order Functions`.

Higher-order functions are particulary useful in lots of cases. Assume you have an algorithm that takes and array of values as an input:

## Resources

Primitive types, Unit, tuples, functions (functions without parameters, how functions return result, functions with Unit return result), functions as values, nested functions, anonymous functions, list expression, array expression, “for” expressions, if..then..else expressions.

https://fsharpforfunandprofit.com/posts/understanding-fsharp-expressions/

https://www.matthewgerstman.com/tech/functional-programming-fundamentals/

F# script files, loading files, loading libraries, using Paket, running the scripts, etc.

https://brandewinder.com/2016/02/06/10-fsharp-scripting-tips/
