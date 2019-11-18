module Domain =

    module Skill =

        type Skill = {
            Name: string;
        }

        let create name = { Name = name }

    module Expertise =

        type Level = 
            | Beginner = 1
            | Junior = 2
            | Midlle = 3
            | Senior = 4
            | Expert = 5

        type Skill = {
            Name: string;
            Level: Level;
        }

        type Expertise =
            | NotApproved of Skill
            | Approved of Skill

        let create name level = 
            NotApproved { Name = name; Level = level }

        let changeLevel level expertise = 
            match expertise with
            | NotApproved skill
            | Approved skill -> 
                NotApproved { skill with Level = level }

        let approve expertise = 
            match expertise with
            | NotApproved skill
            | Approved skill -> 
                Approved skill

    module Employee =

        open Expertise

        type Employee = {
            FullName: string;
            Expertise: Expertise list;
        }
        let addExpertise skillName level employee = 
            let expertise = Expertise.create skillName level
            { employee with Expertise = expertise::employee.Expertise }

        let approveSkill skillName employee =
            let matchSkill skillName skill = 
                match skill with
                | NotApproved expertise when expertise.Name = skillName -> 
                    Expertise.approve skill
                | _ -> skill

            let mapSkill = matchSkill skillName            
            { employee with Expertise = List.map mapSkill employee.Expertise }

        let changeLevel skillName level employee =
            let matchSkill skillName skill = 
                match skill with
                | NotApproved expertise
                | Approved expertise when expertise.Name = skillName ->
                    Expertise.changeLevel level skill
                | _ -> skill

            let mapSkill = matchSkill skillName            
            { employee with Expertise = List.map mapSkill employee.Expertise }
                
        let create fullName = { FullName = fullName; Expertise = [] }

module EventSourcing =
    
    let bind func (state, events) =
        let (newState, newEvents) = func state
        (newState, events @ newEvents)

    let (==>) state func = bind func state

    let fromHistory applyEvent empty events = 
        let rec fromEvents applyEvent events entity =
            match events with
            | head::tail -> applyEvent head entity |> fromEvents applyEvent tail
            | [] -> entity
        fromEvents applyEvent events empty

    module Skill =

        open Domain

        type SkillEvent =
            | SkillCreated of Name: string

        let applyEvent event entity =
            match event with
            | SkillCreated name -> Skill.create name

        let empty = Skill.create ""

        let fromHistory = fromHistory applyEvent empty

        let create name = 
            let entity = Skill.create name
            let event = SkillCreated name
            (entity, [event])

    module Employee =

        open Domain
        open Expertise

        type EmployeeEvent =
            | EmployeeCreated of string
            | ExpertiseAdded of string * Level
            | ExpertiseApproved of string
            | ExpertiseLevelChanged of string * Level

        let applyEvent event entity =
            match event with
            | EmployeeCreated fullName -> Employee.create fullName
            | ExpertiseAdded (name, level) -> Employee.addExpertise name level entity
            | ExpertiseApproved skillName -> Employee.approveSkill skillName entity
            | ExpertiseLevelChanged (name, level) -> Employee.changeLevel name level entity

        let empty = Employee.create ""

        let fromHistory = fromHistory applyEvent empty

        let create fullName = 
            let entity = Employee.create fullName
            let event = EmployeeCreated fullName
            (entity, [event])

        let addExpertise skillName level employee = 
            let entity = Employee.addExpertise skillName level employee
            let event = ExpertiseAdded (skillName, level)
            (entity, [event])

        let approveSkill skillName employee =
            let entity = Employee.approveSkill skillName employee
            let event = ExpertiseApproved skillName
            (entity, [event])

        let changeLevel skillName level employee =
            let entity = Employee.changeLevel skillName level employee
            let event = ExpertiseLevelChanged (skillName, level)
            (entity, [event])

module App =

    open Domain.Expertise
    open EventSourcing

    let employee1 =
        Employee.create "Pavlo Hodysh"
        ==> Employee.addExpertise "C#" Level.Senior
        ==> Employee.addExpertise "F#" Level.Senior
        ==> Employee.addExpertise ".NET" Level.Senior
        ==> Employee.approveSkill "C#"
        ==> Employee.approveSkill ".NET"
        ==> Employee.changeLevel "C#" Level.Expert
        ==> Employee.changeLevel ".NET" Level.Expert
        ==> Employee.approveSkill "C#"
        ==> Employee.approveSkill "F#"
        ==> Employee.approveSkill ".NET"

    let events = snd employee1

    let employee2 = Employee.fromHistory events