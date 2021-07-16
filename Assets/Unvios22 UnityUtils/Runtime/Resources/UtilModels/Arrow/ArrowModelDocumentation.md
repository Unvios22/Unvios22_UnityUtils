The arrow model is normalized; it has exactly 1 Unity worldspace unit in length.

To create such a model in blender, make sure the model has exactly 1 unit of length
in blender worldspace units in the desired dimension. Make sure location, rotation
& scale are applied, then export to fbx, using 1.0 scale, setting ApplyScalings to
"FBX Units Scale" and assigning forward & up axis respectively.

When importing into unity, make sure ScaleFactor is 1 and ConvertUnits is disabled.
