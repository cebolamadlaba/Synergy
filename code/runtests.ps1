﻿ForEach ($folder in (Get-ChildItem -Path test -Directory)) { dotnet test $folder.FullName }