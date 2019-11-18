type Reducer<'State, 'Action, 'Result> = 'State -> 'Action -> 'Result

type Actor<'a> = MailboxProcessor<'a>

type Store<'a> = Store of State: 'a

type Message<'State, 'Action> =
    | GetState of AsyncReplyChannel<'State>
    | SetState of 'Action
    | Complete

let createStore state reducer = 
    let starActorAsync (state: 's) (reducer: Reducer<'s, 'a, 's>) = 
        let handleAsync (mailbox: Actor<Message<'s, 'a>>) = 
            let rec handleRecursive state = async {
                let! message = mailbox.Receive()
                match message with
                | GetState channel ->
                    channel.Reply state
                    return! handleRecursive state
                | SetState action ->                    
                    let nextState = reducer state action
                    return! handleRecursive nextState
                | Complete -> ()
            }
            handleRecursive state
        Actor<Message<'s, 'a>>.Start handleAsync
    Store (starActorAsync state reducer)

let dispatch (Store (actor: Actor<Message<'s, 'a>>)) (action: 'a) = 
    actor.Post(SetState action)

let getState (Store (actor: Actor<Message<'s, 'a>>)) =
    actor.PostAndReply(GetState)
