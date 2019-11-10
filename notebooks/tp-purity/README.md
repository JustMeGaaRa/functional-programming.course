# Functional Purity

A pure function is a function that has no side-effects. Basically, not having a side-effect means not mutating any external state. If the function mutates any external state, that it has some implicit results. Any of the implicit side-effects can be either an IO output, or multithreading, or some king of network connection, SQL connections, exceptions and many more. But what happens, if this external state prodiuces an error?

Let's have a look at different aspects of side-effects.

## Shared State

Let's have a look at the following function:


```C#
let getTimeDifference (date: DateTime) = DateTime.UtcNow - date
let someFutureTime = DateTime.Parse "1-1-2020"
let difference0 = getTimeDifference someFutureTime
printf "%A" difference0
```

    -52.00:09:22.7772678




<null>



But what if we run it again?


```C#
let difference1 = getTimeDifference someFutureTime
printfn "%A" difference1
```

    -52.00:09:17.1993006
    




<null>



We can see, that each time we call this function with same parameter, the result is different. Not so very predictible result, right? This is because our function `getTimeDifference` is actually dependent on a shared state `DateTime.UtcNow`.

I order for this function to be pure, we need to pass both parameters - from and to. Let's have a look at the example.


```C#
let getTimeDifference (fromDate: DateTime) (toDate: DateTime) = fromDate - toDate
let fromDate = DateTime.Parse "10-1-2020"
let currentDate = DateTime.Parse "1-1-2020"
let difference1 = getTimeDifference fromDate currentDate
printfn "%A" difference1
```

    9.00:00:00
    




<null>



Now let's try and call this function several more times.


```C#
let difference2 = getTimeDifference fromDate currentDate
printfn "%A" difference2
```

    9.00:00:00
    




<null>




```C#
let difference3 = getTimeDifference fromDate currentDate
printfn "%A" difference3
```

    9.00:00:00
    




<null>



So here wee are, the same result every time. Now this function is testable, predictable and can be used as a simple value.

## State Mutation



## Referential Transparency


```C#

```

## Resources

Explicit side-effects, function purity, referential transparency principle. Classes, constructors, instance members, static members, mutability, operators, generics, properties, overloading, access modifiers, inheritance, etc.

https://fsharpforfunandprofit.com/series/thinking-functionally.html

https://fsharpforfunandprofit.com/fppatterns/

Parts, Nested modules, Namespaces

https://fsharpforfunandprofit.com/posts/recipe-part3/



```C#

```
