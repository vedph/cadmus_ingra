# Cadmus Inquisition Graffiti

## Models

### Prison Item

- `PrisonInfoPart`\* (`it.vedph.ingra.prison-info`)
- `BibliographyPart` (`it.vedph.bibliography`)
- `DocReferencesPart` (`it.vedph.itinera.doc-references`)

### Prisoner Item

- `PrisonerInfoPart`\* (`it.vedph.ingra.prisoner-info`)
- `BibliographyPart` (`it.vedph.bibliography`)

### Graffiti Item

- `GraffitiInfoPart`\* (`it.vedph.ingra.graffiti-info`)
- `PrisonLocationPart`\* (`it.vedph.ingra.prison-location`)
- `TextPart`\*: normalized text. (`it.vedph.token-text`)
- `BibliographyPart` (`it.vedph.bibliography`)
- `CategoriesPart`  (`it.vedph.categories`)
- `CommentPart` (`it.vedph.comment`)
- `NotePart` with role=`palg`: paleographic note. (`it.vedph.note`)

Layers:

- `ApparatusLayerPart` (`fr.it.vedph.apparatus`)
- `CommentLayerPart` (`fr.it.vedph.comment`)

### Drawing Item

- `DrawingInfoPart`\* (`it.vedph.ingra.drawing-info`)
- `PrisonLocationPart`\* (`it.vedph.ingra.prison-location`)
- `BibliographyPart` (`it.vedph.bibliography`)

### Parts

These are the parts specific to this project.

#### PrisonInfoPart

- `prisonId`\* (`string`)
- `place`\* (`string`)

#### GraffitiInfoPart

- `graffitiId`\* (`string`)
- `language`\* (`string`: thesaurus: `graffiti-languages`)
- `verse` (`string`: thesaurus: `graffiti-verses`)
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
  - `language`\* (`string` = code from [ISO 639-3](https://en.wikipedia.org/wiki/ISO_639-3), thesaurus: `languages`)
  - `tag` (`string`, optionally from thesaurus: `person-name-tags`): optional tag used to group names, typically used when a person has several names.
  - `parts`\* (`PersonNamePart[]`):
  - `type`\* (`string`, thesaurus: `person-name-types`): e.g. first name, last name, etc. Types are not unique in a name: for instance, you might have a person with 2 first names.
  - `value`\* (`string`)
- `birthDate` (`HistoricalDate`)
- `deathDate` (`HistoricalDate`)
- `origin` (`string`)
- `charge` (`string`, thesaurus: `trial-charges`)
- `judgement` (`string`, thesaurus: `trial-judgements`)
- `detentionStart` (`HistoricalDate`)
- `detentionEnd` (`HistoricalDate`)

#### DrawingInfoPart

- `description`\* (string, MD)
- `subjects`\* (hierarchical thesaurus: `drawing-subjects`)
- `date` (HistoricalDate)
- `color`\* (string, thesaurus: `drawing-colors`)
- `links` (`TaggedId`[]):
  - `id`\* (string): graffiti ID.
  - `tag`\* (string, thesaurus: `link-reasons`): the reason for the link.
