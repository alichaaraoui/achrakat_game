# Temple Recharging & Bridge Construction System Setup Guide

This guide will help you set up the temple recharging system with segmented bridge construction in Unity.

## 📋 Required Scripts

1. **Temple.cs** - Individual temple behavior
2. **GameManager.cs** - Manages all temples and triggers bridge construction
3. **BridgeSequencer.cs** - Handles bridge segment appearance
4. **EnergyManager.cs** - Manages player energy (already exists)

## 🏗️ Scene Setup Instructions

### Step 1: Create Temple Objects

1. **Create Temple GameObjects:**
   - Create empty GameObjects for each temple
   - Name them "Temple1", "Temple2", etc.
   - Position them where you want the temples to be

2. **Add Temple Components:**
   - Add the `Temple` script to each temple
   - Add a **Collider** component (set to "Is Trigger")
   - Add a **Renderer** component (Mesh Renderer)
   - Add an **AudioSource** component (optional)

3. **Configure Temple Settings:**
   ```
   Energy Required: 10 (or your desired amount)
   Recharge Time: 2.0 (seconds)
   Uncharged Material: [Assign dark/dull material]
   Charged Material: [Assign bright/glowing material]
   Temple Light: [Assign Light component]
   ```

### Step 2: Create Bridge Structure

1. **Create Bridge Parent:**
   - Create an empty GameObject named "Bridge"
   - Add the `BridgeSequencer` script to it

2. **Create Bridge Segments:**
   - Create child GameObjects under "Bridge"
   - Name them "Segment1", "Segment2", "Segment3", etc.
   - Add 3D meshes (cubes, planes, or custom models) to each segment
   - Position them to form your bridge

3. **Configure BridgeSequencer:**
   ```
   Delay Between Segments: 0.5 (seconds)
   Pop Animation Duration: 0.3 (seconds)
   Max Scale: 1.2 (for pop effect)
   Auto Find Segments: ✓ (enabled)
   ```

### Step 3: Setup GameManager

1. **Create GameManager:**
   - Create an empty GameObject named "GameManager"
   - Add the `GameManager` script to it

2. **Configure GameManager:**
   - Drag the "Bridge" GameObject to the "Bridge Sequencer" field
   - (Optional) Assign UI elements for progress display

### Step 4: Setup EnergyManager

1. **Create EnergyManager:**
   - Create an empty GameObject named "EnergyManager"
   - Add the `EnergyManager` script to it

2. **Configure Energy Settings:**
   ```
   Current Energy: 0 (starting energy)
   Max Energy: 100 (maximum energy)
   ```

### Step 5: Setup Player

1. **Ensure Player Setup:**
   - Make sure your player has the "Player" tag
   - Ensure player has a Collider component
   - Player should be on the correct layer

### Step 6: Create Materials

1. **Create Temple Materials:**
   - Create a material for uncharged temples (dark/gray)
   - Create a material for charged temples (bright/glowing)
   - Assign these to the Temple scripts

2. **Create Bridge Materials:**
   - Create materials for your bridge segments
   - Assign them to the bridge segment renderers

## 🎮 How It Works

### Temple Recharging Process:
1. **Player approaches temple** → Enters trigger collider
2. **Player presses E** → Starts recharge if they have enough energy
3. **Temple recharges** → Material changes from uncharged to charged
4. **Temple notifies GameManager** → GameManager tracks progress
5. **When all temples recharged** → GameManager calls BridgeSequencer

### Bridge Construction Process:
1. **BridgeSequencer starts** → All segments are initially disabled
2. **Segments appear one by one** → With delay between each
3. **Each segment pops** → Scale animation when appearing
4. **Effects play** → Particle effects and sound for each segment
5. **Bridge complete** → All segments are visible and functional

## 🔧 Configuration Options

### Temple Settings:
- **Energy Required**: How much energy each temple needs
- **Recharge Time**: How long the recharge animation takes
- **Materials**: Visual feedback for charged/uncharged states
- **Light**: Optional light that activates when charged

### Bridge Settings:
- **Delay Between Segments**: Time between each segment appearing
- **Pop Animation**: Scale animation when segments appear
- **Effects**: Particle systems and audio for segment appearance

### GameManager Settings:
- **Auto-find Temples**: Automatically finds all Temple objects
- **UI Integration**: Progress display for temple recharging

## 🐛 Debug Features

### Temple Debug:
- Check console for recharge messages
- Verify trigger colliders are set up correctly
- Test energy system with debug methods

### Bridge Debug:
- Right-click BridgeSequencer → "Test Bridge Construction"
- Right-click BridgeSequencer → "Reset Bridge"
- Check console for construction progress

### GameManager Debug:
- Right-click GameManager → "Force Bridge Construction"
- Right-click GameManager → "Reset All Temples"
- Monitor temple count and progress

## 🎯 Testing Checklist

- [ ] Player can approach temples
- [ ] Player can press E to recharge temples
- [ ] Temples change color when recharged
- [ ] GameManager tracks all temples
- [ ] Bridge segments appear one by one
- [ ] Pop animations work correctly
- [ ] Effects play for each segment
- [ ] Bridge is fully functional when complete

## 🔧 Troubleshooting

### Common Issues:

1. **Temples not responding to E key:**
   - Check player has "Player" tag
   - Verify trigger colliders are set up
   - Ensure Temple script is attached

2. **Bridge not appearing:**
   - Check GameManager has BridgeSequencer assigned
   - Verify bridge segments are children of Bridge object
   - Check console for error messages

3. **No energy system:**
   - Ensure EnergyManager exists in scene
   - Use debug methods to add energy for testing
   - Check energy requirements vs available energy

4. **UI not updating:**
   - Verify UI elements are assigned to GameManager
   - Check that UI scripts are properly configured

## 📝 Example Scene Hierarchy

```
Scene
├── GameManager
├── EnergyManager
├── Player
├── Temple1
│   ├── Temple (Script)
│   ├── Collider (Trigger)
│   ├── Renderer
│   └── Light
├── Temple2
│   ├── Temple (Script)
│   ├── Collider (Trigger)
│   ├── Renderer
│   └── Light
├── Bridge
│   ├── BridgeSequencer (Script)
│   ├── Segment1
│   ├── Segment2
│   ├── Segment3
│   └── Segment4
└── UI Canvas
    ├── Progress Text
    └── Progress Slider
```

This setup will give you a fully functional temple recharging system with segmented bridge construction!
