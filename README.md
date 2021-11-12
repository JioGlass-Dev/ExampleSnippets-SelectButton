# Select Button
Select button implementation of JMRSDK

## Scripts 

### `Gun.cs`
Shows example of accessing callbacks when select button is pressed down, released and clicked.</br>
- Interfaces to implement select button funtionalities
```cs
public class SelectButton : MonoBehaviour, ISelectHandler, ISelectClickHandler
```
- Methods to implement select related funtionality
```cs
public void OnSelectClicked(SelectClickEventData eventData)
{ }
public void OnSelectDown(SelectEventData eventData)
{ }
public void OnSelectUp(SelectEventData eventData)
{ }
```

## How to use?
1. Download and unzip this project.
2. Open the project using Unity Hub.
3. Download and import the latest version of JMRSDK package.
4. Open and play the SelectGun scene from Assets/Scenes folder.
