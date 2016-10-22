BDD + UI Automation = Conslugo.Automation
=========================================

# What is it?

Consulgo.Tests is an experiment in using Reactive Extensions to produce reactive tests that are readable and hide boiler plate. This is Proof-Of-Concept, that it's possible to implement reactive approach for both BDD and Windows Automation instead of the ugly repeatable code.

# Why?

SpecFlow is very good tool. This is not a goal to replace it, but rather to evaluate different approach.
SpecFlow is using synchronous approach, and this combined with Windows Automation often gives smelly code (e.g. Thread.Sleep). Such code is difficult to understand, and many times it is needed to construct own framework (e.g. based on White) to reduce number of places with boilerplate code. I evaluated many times different approaches to the same problem, and this is another attempt for Reactive BDD and Reactive Windows Automation.
Both Reactive BDD and Reactive Windows Automation are shipped in same solution (with sample test apps using both), but can be used separately.

# How does it work?

## Reactive BDD

* System under tests is encapsulated in a model
* All steps are commands
* State of the model is passed as parameter
* Result of each command is an observable
* Commands can have pre- and post- requisites

## Reactive Automation

* All controls have their own classes
* All controls have same base class
* All changes (structure, properties) to the controls are exposed on observables


# Future work

This is only PoC, so implement more features and unit tests.

# Samples

There is a little more advanced Hello World application (called Ugly Yet Another Application), where tests are evaluated from second executable (Consulgo.Test.ReactiveAutomation.SampleRunner). Sample application is started, tested and killed.
