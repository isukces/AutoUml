# What is AutoUML

Need UML diagrams directly from .NET source code? AutoUML is for you.
AutoUML uses internally [PlantUML](https://plantuml.com/).

# Features
- create UML diagrams from any .NET code annotation
- export to PNG o SVG files

# Example
![alt text](https://raw.githubusercontent.com/isukces/AutoUml/master/docs/testsResults/T12_Should_mark_static_method.png "Sample diagram created during AutoUML testing")

Some other examples containing both source PUML files and images can be found here: https://github.com/isukces/AutoUml/tree/master/docs/testsResults .


# How it works?

UML diagram creation process consists of few steps:
- preparing UML project with prefered settings (some default settings are provided)
- scanning source code for annotations
- preparing PUML files (see [PlantUML](https://plantuml.com/))
- converting PUML files into images
