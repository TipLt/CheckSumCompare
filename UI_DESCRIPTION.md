# CheckSumCompare - User Interface Description

## Window Layout

The application features a clean, modern WPF interface with the following layout:

### Window Properties
- **Title**: "CheckSum Compare"
- **Size**: 700x550 pixels
- **Position**: Centered on screen
- **Resizable**: No (fixed size for consistent UX)

### Main Sections (Top to Bottom)

#### 1. Application Title
- Large, bold text: "CheckSum Compare Tool"
- Centered at the top
- Font Size: 24pt

#### 2. File Selection Group
- **Label**: "File Selection"
- **Components**:
  - Read-only text box displaying selected file path
  - "Browse..." button on the right
- **Action**: Opens Windows file dialog to select any file

#### 3. Hash Algorithm Selection Group
- **Label**: "Hash Algorithm"
- **Component**: Dropdown/ComboBox with options:
  - MD5
  - SHA1
  - SHA256 (Default selection)
  - SHA384
  - SHA512

#### 4. Calculate Button
- Large prominent button: "Calculate Checksum"
- Bold text, slightly larger font
- Full width
- **Action**: Computes the hash of the selected file
- **Behavior**: Disables all controls during calculation to prevent conflicts

#### 5. Calculated Checksum Group
- **Label**: "Calculated Checksum"
- **Component**: Read-only text box
- **Font**: Monospace (Consolas) for easy reading of hex values
- Displays the computed hash in uppercase hexadecimal format

#### 6. Expected Checksum Group
- **Label**: "Expected Checksum (Optional)"
- **Components**:
  - Text box for user input (monospace font)
  - "Compare" button on the right
- **Action**: Compares calculated vs expected checksum

#### 7. Results Area
- **Label**: "Results"
- **Component**: Large, read-only text area
- **Font**: Monospace (Consolas)
- **Background**: Light gray (#F5F5F5) by default
- **Scrollable**: Yes (vertical scrollbar if needed)
- **Dynamic Coloring**:
  - **Green background** (#C8FFC8) when checksums match with ✓ symbol
  - **Red background** (#FFC8C8) when checksums don't match with ✗ symbol

## User Experience Features

### Visual Feedback
1. **During Calculation**:
   - All controls disabled
   - "Calculating..." text displayed
   - Prevents user interaction during processing

2. **Comparison Results**:
   - Clear visual indicators (colors + symbols)
   - Detailed text explanation
   - Side-by-side checksum display

### Format Flexibility
The comparison is intelligent and handles various input formats:
- Case-insensitive (ABC123 = abc123)
- Ignores hyphens (AB-CD-EF = ABCDEF)
- Ignores spaces (AB CD EF = ABCDEF)

### Error Handling
- Clear error messages via message boxes
- Validation checks before operations:
  - File must be selected
  - File must exist
  - Checksum must be calculated before comparison
  - Expected checksum must be provided for comparison

## Color Scheme
- **Primary Background**: White
- **Group Backgrounds**: Light gray (#F5F5F5)
- **Success State**: Light green (#C8FFC8)
- **Error State**: Light red/pink (#FFC8C8)
- **Text**: Black on light backgrounds for maximum readability

## Typical Workflow Visualization

```
[CheckSum Compare Tool]
┌─────────────────────────────────────────────────┐
│              CheckSum Compare Tool              │
├─────────────────────────────────────────────────┤
│ ┌─ File Selection ──────────────────────────┐  │
│ │ [C:\path\to\file.exe      ] [Browse...]  │  │
│ └───────────────────────────────────────────┘  │
│                                                 │
│ ┌─ Hash Algorithm ──────────────────────────┐  │
│ │ [SHA256                   ▼]              │  │
│ └───────────────────────────────────────────┘  │
│                                                 │
│ ┌──────────────────────────────────────────┐   │
│ │      Calculate Checksum                  │   │
│ └──────────────────────────────────────────┘   │
│                                                 │
│ ┌─ Calculated Checksum ─────────────────────┐  │
│ │ A1B2C3D4E5F6...                           │  │
│ └───────────────────────────────────────────┘  │
│                                                 │
│ ┌─ Expected Checksum (Optional) ────────────┐  │
│ │ [paste expected hash     ] [Compare]     │  │
│ └───────────────────────────────────────────┘  │
│                                                 │
│ ┌─ Results ─────────────────────────────────┐  │
│ │ ✓ MATCH - Checksums are identical!       │  │
│ │                                           │  │
│ │ Calculated: A1B2C3D4E5F6...             │  │
│ │ Expected:   A1B2C3D4E5F6...             │  │
│ │                                           │  │
│ │ The file integrity is verified.          │  │
│ └───────────────────────────────────────────┘  │
└─────────────────────────────────────────────────┘
```

## Accessibility
- Clear labels for all input areas
- Grouped controls for logical organization
- Large, readable fonts
- High contrast color scheme
- Keyboard navigation support (standard Windows controls)
