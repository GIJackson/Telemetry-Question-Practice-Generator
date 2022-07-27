# Telemetry-Question-Practice-Generator

The purpose of this project is to create an application that generates questions to test the user's knowledge of telemetry wave strips. The application will be able to create a test that includes the heart rhythms with which a person can sustain life without immediate need for intervention, heart rhythms that if not treated can lead to injury or death, and heart rhythms that signal an immediate need for intervention. After indicating to the console to begin the test, the application will generate a single ASCII image that resembles a 10-second telemetry strip. The user must then type in what rhythm the image represents. The user will have the option to quit out after each question as well as start a new question.

The hope is that this project will include the following features in its code:
<ul>
<li>A “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program.</li>
<li>A dictionary or list, populated with several values, retrieves at least one value, and uses it in the program </li>
<li>Uses a LINQ query to retrieve information from a data structure (such as a list or array) or file</li>
<li>~~Visualize data in a graph, chart, or other visual representation of data~~ This was a bonus feature, unable to implement at this time (unless my ScoreLinq class counts), plan is to integrate facts, stats, and information about each rhythm for the user to read</li>
<li>Implements a log that records important events (tests and their related information) and writes them to a text file</li>
</ul>

At this time, the application is only able to run on Windowsand is operable on the command line. Future work on the project will include efforts to create macOS and linux compatability.
