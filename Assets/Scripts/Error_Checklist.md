# Error Checklist for Temple System

## 🔍 **Step 1: Add DebugHelper to Scene**

1. Create an empty GameObject named "DebugHelper"
2. Add the `DebugHelper` script to it
3. Play the scene and check the Console for setup errors

## 🚨 **Common Errors & Solutions**

### **Error: "EnergyManager not found"**
**Solution:**
- Create empty GameObject named "EnergyManager"
- Add `EnergyManager` script to it
- Set starting energy (e.g., 100)

### **Error: "GameManager not found"**
**Solution:**
- Create empty GameObject named "GameManager"
- Add `GameManager` script to it
- Assign BridgeSequencer in inspector

### **Error: "No GameObject with 'Player' tag found"**
**Solution:**
- Select your player GameObject
- In Inspector, set Tag to "Player"
- Ensure player has a Collider component

### **Error: "Temple missing Collider component"**
**Solution:**
- Select each temple GameObject
- Add Collider component (Box Collider, Sphere Collider, etc.)
- Check "Is Trigger" checkbox

### **Error: "BridgeSequencer not found"**
**Solution:**
- Create empty GameObject named "Bridge"
- Add `BridgeSequencer` script to it
- Create child objects for bridge segments
- Name them "Segment1", "Segment2", etc.

### **Error: "Material.Lerp is not a valid method"**
**Solution:**
- This should be fixed in the updated Temple.cs script
- Make sure you're using the latest version

### **Error: "NullReferenceException"**
**Common Causes:**
- Missing materials assigned in Temple inspector
- Missing AudioSource components
- Missing Light components

**Solution:**
- Assign uncharged and charged materials to temples
- Add AudioSource components if using audio
- Add Light components if using lights

## 🎯 **Quick Test Setup**

### **Minimal Scene Setup:**
```
Scene
├── EnergyManager (GameObject + EnergyManager script)
├── GameManager (GameObject + GameManager script)
├── Player (GameObject with "Player" tag + Collider)
├── Temple1 (GameObject + Temple script + Trigger Collider)
├── Bridge (GameObject + BridgeSequencer script)
│   ├── Segment1 (Child GameObject with Mesh)
│   └── Segment2 (Child GameObject with Mesh)
└── DebugHelper (GameObject + DebugHelper script)
```

### **Temple Setup:**
1. Create temple GameObject
2. Add `Temple` script
3. Add Collider (set to "Is Trigger")
4. Add Mesh Renderer
5. Create two materials (dark and bright)
6. Assign materials to Temple script

### **Bridge Setup:**
1. Create "Bridge" GameObject
2. Add `BridgeSequencer` script
3. Create child objects named "Segment1", "Segment2", etc.
4. Add 3D meshes to segments (cubes, planes, etc.)

## 🔧 **Debug Commands**

Right-click on scripts in Inspector for these debug options:

### **EnergyManager:**
- "Add 50 Energy"
- "Set Energy to Max"

### **GameManager:**
- "Force Bridge Construction"
- "Reset All Temples"

### **BridgeSequencer:**
- "Test Bridge Construction"
- "Reset Bridge"

### **DebugHelper:**
- "Run Setup Check"
- "Add Energy"
- "Force Bridge Construction"

## 📋 **Testing Checklist**

- [ ] DebugHelper shows no errors in Console
- [ ] Player has "Player" tag
- [ ] Temples have trigger colliders
- [ ] EnergyManager exists and has energy
- [ ] GameManager exists and finds temples
- [ ] BridgeSequencer exists and finds segments
- [ ] Materials are assigned to temples
- [ ] Bridge segments are children of Bridge object

## 🆘 **Still Having Issues?**

1. **Check Console** for specific error messages
2. **Use DebugHelper** to identify missing components
3. **Test with debug commands** to isolate issues
4. **Verify scene hierarchy** matches the setup guide

## 📞 **Common Error Messages & Solutions**

| Error Message | Solution |
|---------------|----------|
| "NullReferenceException" | Check all assigned references in Inspector |
| "EnergyManager not found" | Create EnergyManager GameObject |
| "GameManager not found" | Create GameManager GameObject |
| "No bridge segments found" | Add child objects to BridgeSequencer |
| "Player tag not found" | Set player GameObject tag to "Player" |
| "Collider is not trigger" | Check "Is Trigger" on temple colliders |
