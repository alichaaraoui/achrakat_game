# Temple Recharging System

This system allows players to recharge temples by pressing a button, and when all temples are recharged, a bridge appears.

## Components Overview

### 1. Temple Script
- **Purpose**: Handles individual temple recharging mechanics
- **Key Features**:
  - Visual feedback during recharge (material changes, scaling)
  - Particle effects and audio
  - Light activation when fully charged
  - Event system for custom actions

### 2. TempleManager Script
- **Purpose**: Manages all temples and bridge appearance
- **Key Features**:
  - Tracks recharge progress across all temples
  - Controls bridge appearance animation
  - Provides progress information for UI

### 3. TempleInteraction Script
- **Purpose**: Handles player interaction with temples
- **Key Features**:
  - Proximity detection
  - UI prompts for interaction
  - Visual highlighting
  - Input handling (E key by default)

### 4. EnergyManager Script (Updated)
- **Purpose**: Manages player energy system
- **Key Features**:
  - Energy tracking with UI support
  - Events for energy changes
  - Debug methods for testing

### 5. TempleProgressUI Script
- **Purpose**: Displays temple progress and bridge status
- **Key Features**:
  - Progress bar and text
  - Bridge status updates
  - Real-time UI updates

### 6. EnergyPickup Script
- **Purpose**: Provides energy collection for players
- **Key Features**:
  - Floating animation
  - Visual effects
  - Audio feedback

## Setup Instructions

### Step 1: Create Temple Objects
1. Create empty GameObjects for each temple
2. Add the `Temple` script to each temple
3. Add the `TempleInteraction` script to each temple
4. Configure the temple settings:
   - Assign temple model (3D mesh)
   - Set energy required
   - Set recharge time
   - Assign materials (uncharged/charged)
   - Add particle effects and lights

### Step 2: Setup TempleManager
1. Create an empty GameObject named "TempleManager"
2. Add the `TempleManager` script
3. Assign the bridge object
4. Configure bridge appearance settings
5. Add audio sources and effects

### Step 3: Setup EnergyManager
1. Create an empty GameObject named "EnergyManager"
2. Add the `EnergyManager` script
3. Configure energy settings (max energy, starting energy)
4. Assign UI elements if using UI

### Step 4: Create UI Elements
1. Create Canvas for UI
2. Add progress text and slider
3. Add interaction prompts
4. Add the `TempleProgressUI` script to manage UI updates

### Step 5: Create Energy Pickups
1. Create pickup objects in the world
2. Add the `EnergyPickup` script
3. Configure pickup settings and effects
4. Ensure player has "Player" tag

### Step 6: Configure Player
1. Ensure player has a Collider with "Player" tag
2. Add Rigidbody if needed for physics
3. Ensure player layer is set correctly

## Configuration Options

### Temple Settings
- `energyRequired`: Amount of energy needed to recharge
- `rechargeTime`: Time it takes to recharge (seconds)
- `templeModel`: 3D model of the temple
- `unchargedMaterial`: Material when temple is not charged
- `chargedMaterial`: Material when temple is fully charged
- `rechargeEffect`: Particle system for recharge animation
- `templeLight`: Light that activates when charged

### Interaction Settings
- `interactionRange`: Distance player can interact from
- `interactKey`: Key to press for interaction (default: E)
- `playerLayer`: Layer mask for player detection
- `promptMessage`: Text shown when player can interact
- `noEnergyMessage`: Text shown when player lacks energy

### Bridge Settings
- `bridgeObject`: The bridge GameObject to appear
- `bridgeAppearTime`: Time for bridge appearance animation
- `bridgeMaterial`: Material for bridge (optional)
- `bridgeAppearEffect`: Particle effect for bridge appearance

### Energy Settings
- `maxEnergy`: Maximum energy player can have
- `currentEnergy`: Starting energy amount
- `energyText`: UI Text for energy display
- `energySlider`: UI Slider for energy bar

## Usage

1. **Player collects energy** from pickups scattered in the world
2. **Player approaches a temple** and sees interaction prompt
3. **Player presses E** to start recharging the temple
4. **Temple recharges** with visual and audio feedback
5. **When all temples are recharged**, the bridge appears
6. **Player can cross the bridge** to progress

## Debug Features

- **EnergyManager**: Right-click in inspector for "Add 50 Energy" and "Set Energy to Max"
- **TempleManager**: Right-click in inspector for "Force Bridge Appearance"
- **Console logs**: Detailed progress information in console

## Tips

1. **Test with debug methods** to quickly test the system
2. **Adjust interaction ranges** based on your game's scale
3. **Use particle effects** to make recharging more visually appealing
4. **Add audio feedback** for better player experience
5. **Configure materials** to clearly show charged vs uncharged states
6. **Use lights** to make charged temples more visible

## Troubleshooting

- **Temples not found**: Ensure TempleManager can find temples in scene
- **No interaction**: Check player tag and layer settings
- **No energy**: Use debug methods to add energy for testing
- **Bridge not appearing**: Check bridge object assignment in TempleManager
- **UI not updating**: Ensure TempleProgressUI is properly configured
