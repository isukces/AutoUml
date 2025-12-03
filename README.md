# AutoUML

AutoUML generates PlantUML diagrams straight from your .NET code. Mark the members you want to show, run the generator, and get ready‑to‑use PUML, PNG, or SVG outputs.

## Highlights
- turn annotated .NET code into UML quickly
- exports PUML, PNG, and SVG
- works with any PlantUML toolchain
- annotations stay out of release builds by default (see below)

## Annotations and conditional compilation
Every attribute provided by this library is declared with `Conditional("AUTOUML_ANNOTATIONS")`. Add the `AUTOUML_ANNOTATIONS` compilation symbol when you want the attributes emitted; without it, the attributes are skipped, so your production assemblies remain clean while keeping the source markup intact.

## Example
![alt text](https://raw.githubusercontent.com/isukces/AutoUml/master/docs/testsResults/T12_Should_mark_static_method.png "Sample diagram created during AutoUML testing")

More examples (PUML + generated images): https://github.com/isukces/AutoUml/tree/master/docs/testsResults

## How it works
1. Configure an AutoUML project (defaults are provided).
2. Scan source code for the annotations.
3. Produce PUML files via [PlantUML](https://plantuml.com/).
4. Render PUML to images.
