## Shapes
1. Add __EventSystem__.
2. Inside of Main Camera create an empty __GameObject__ (rename to _LineParent_).
3. Inside of Main Camera create a __Canvas__ (_DotsCanvas_).
4. In Canvas change _Render Mode_ to "Screen Space - Camera".
5. Set _Render Camera_ (add Main Camera).
6. Create a __Canvas__ (_PenToolCanvas_), place it between LineParent and DotsCanvas. Set _Render Mode_ ("Screen Space - Camera")". Set _Render Camera_ (add Main Camera).
7. Inside of PenToolCanvas create an __Image__ (_PenCanvas_), stretch it, change its color.
8. Inside of PenToolCanvas create an __Image__, anchored it to a left up corner, set its position (50, -50), change its sprite.
9. Create a new script (_PenTool_). Add logic. Add it as PenToolCanvas's component.
10. Create a _Prefabs_ folder.
11. Inside of DotsCanvas create an __Image__ (_DotPrefab_). Change its sprite, size, color, etc. Place it into the Prefabs folder.
12. Delete it from DotsCanvas.
13. Inside of LineParent create an empty __GameObject__ (_LinePrefab_). Add it a _LineRenderer_ component. Change its size, width, matherial (_Default-Line_). Place it into the Prefabs folder. Delete it from LineParent.
14. Open LinePrefab, add a new script (_LineController_).
15. Add a new script (_PenCanvas_) to PenCanvas. Add logic.
16. Add a new script (_DotController_) to DotPrefab. Add logic.