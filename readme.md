Ambitious little 4-way chess app.

[Link to editable Gantt Chart](https://drive.google.com/file/d/0B_1XCZCsajMsVUNyaHNTS2lndVU/view?usp=sharing) (revised 11/4/17)

Most of the project documentation is on the Wiki attached to this repository.

*Notice* The tests can only be ran from the server solution file in visual studio.
*Notice* This project consists of two parts: A Unity project and a Visual Studio solution. The Visual Studio solution **must be built first**. If this procedure is not followed, Unity will not build.

File Hierarchy Overview
* CHESSx4
  * *The frontend (Unity Project)*
  * *Contains scripes for all drawing logic*
  * Assets
    * *Where the assets are located*
    * Pieces
      * *The PNGs that are drawn for pieces*
    * Plugins
      * *The compiled backend for the Unity Project*
    * Squares
      * *The PNGs for the checkerboard*
    * UI
      * *Assets for the menu_scene scene*
  * ProjectSettings
    * *Unity Files*
* Documentation
* Server
  * *The Backend*
  * Src
    * *Holds the source for the backend*
    * GameModel.Client
      * *Holds the game model used for local play and generating potential moves in network play*
    * GameModel.Messages
      * *Holds the classes used for communication between Unity and the backend*
    * GameModel.Server
      * *Holds the code used to create a network server that will delegate four instances of the game* 
  * Tests
    * *Holds the tests for the backend's components*
    
