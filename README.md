# Cadmus Inquisition Graffiti

## Models

### Prison Item

- `PrisonInfoPart`\*
- `BibliographyPart`
- `DocReferencesPart`

### Prisoner Item

- `PrisonerInfoPart`\*
- `BibliographyPart`

### Graffiti Item

- `GraffitiInfoPart`\*
- `PrisonLocationPart`\*
- `TextPart`\*: normalized text.
- `BibliographyPart`
- `CategoriesPart`
- `CommentPart`
- `NotePart` with role=`palg`: paleographic note.

Layers:

- `ApparatusLayerPart`
- `CommentLayerPart`

### Drawing Item

- `DrawingInfoPart`\*
- `PrisonLocationPart`\*
- `BibliographyPart`

### Parts

These are the parts specific to this project.

#### PrisonInfoPart

- `prisonId`\* (`string`)
- `place`\* (`string`)

#### GraffitiInfoPart

- `graffitiId`\* (`string`)
- `language`\* (`string`)
- `verse` (`string`)
- `rhyme` (`string`)
- `author` (`string`)
- `identifications` (`RankedId[]`):
  - `id` (`string`)
  - `rank` (`short`)
- `date` (`HistoricalDate`)

#### PrisonLocationPart

- `prisonId`\* (`string`)
- `cell`\* (`string`)
- `location`\* (`string`)

#### PrisonerInfoPart

- `prisonerId`\* (`string`)
- `prisonId`\* (`string`)
- `sex` (`string`): `M`, `F`, or unknown.
- `name`\* (`PersonName`, same as in Itinera):
  - `language`\* (`string` = code from [ISO 639-3](https://en.wikipedia.org/wiki/ISO_639-3), thesaurus)
  - `tag` (`string`, optionally from thesaurus): optional tag used to group names, typically used when a person has several names.
  - `parts`\* (`PersonNamePart[]`):
  - `type`\* (`string`, thesaurus): e.g. first name, last name, etc. Types are not unique in a name: for instance, you might have a person with 2 first names.
  - `value`\* (`string`)
- `birthDate` (`HistoricalDate`)
- `deathDate` (`HistoricalDate`)
- `origin` (`string`)
- `charge` (`string`, thesaurus)
- `judgement` (`string`, thesaurus)
- `detentionStart` (`HistoricalDate`)
- `detentionEnd` (`HistoricalDate`)

#### DrawingInfoPart

- `description`\* (string, MD)
- `subjects`\* (hierarchical thesaurus)
- `date` (HistoricalDate)
- `color`\* (string, thesaurus)
- `links` ([]):
  - `graffitiId`\* (string)
  - `reason`\* (string, thesaurus)
