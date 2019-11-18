#load "./redux.fsx"

type EmailAddress = {
    Address: string;
}

type PhoneNumber = {
    Number: string;
}

type Contact = {
    Email: EmailAddress;
    Phone: PhoneNumber;
}

type User = {
    Username: string;
    FullName: string;
}

type State = {
    User: User;
    Contact: Contact;
}

type Action =
    | ChangeUserName of string
    | ChangeFullName of string
    | ChangeEmail of string
    | ChangePhone of string

let state = {
    User = {
        Username = "linus.torvalds";
        FullName = "Linus Torvalds";
    };
    Contact = {
        Email = {
            Address = "linus.torvalds@gmail.com";
        };
        Phone = {
            Number = "+180124455777"
        };
    }
}

let phoneReducer state action =
    match action with
    | ChangePhone phone -> { state with Number = phone }
    | _ -> state

let emailReducer state action =
    match action with
    | ChangeEmail email -> { state with Address = email }
    | _ -> state

let contactReducer state action =
    { state with 
        Email = emailReducer state.Email action
        Phone = phoneReducer state.Phone action }
    
let userReducer state action = 
    match action with
    | ChangeUserName name -> { state with Username = name }
    | ChangeFullName name -> { state with FullName = name }
    | _ -> state

let reducer state action =
    { state with 
        User = userReducer state.User action
        Contact = contactReducer state.Contact action }
    

let store = Redux.createStore state reducer

Redux.getState store
Redux.dispatch store (ChangeUserName "john.skeet")
Redux.dispatch store (ChangeFullName "John Skeet")
Redux.dispatch store (ChangeEmail "john.skeet@gmail.com")
Redux.dispatch store (ChangePhone "+180123775999")
Redux.getState store