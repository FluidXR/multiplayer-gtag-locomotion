# Injecting Javascript

If you have javascript you’d like to be injected on page load or upon some kind of event, use this system. 

1. Add a javascript script `<your_script>.js` to the `Resources/JSFiles` folder.
2. The contents of this file will automatically be compiled to a read-only Runtime asset, which you can access like: 

```jsx
JSFilesManager.Instance.GetScript("<your_script>");
```