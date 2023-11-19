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
- `identifications` (`AssertedCompositeId[]`):
  - `target` (`PinTarget`):
	- `gid`\* (`string`)
	- `label`\* (`string`)
	- `itemId` (`string`)
	- `partId` (`string`)
	- `partTypeId` (`string`)
	- `roleId` (`string`)
	- `name` (`string`)
	- `value` (`string`)
  - `scope` (`string`)
  - `tag` (`string`)
  - `assertion` (`Assertion`)
- `date` (`HistoricalDate`)

#### PrisonLocationPart

- `prisonId`\* (`string`)
- `cell`\* (`string`)
- `location`\* (`string`)

#### PrisonerInfoPart

- `prisonerId`\* (`string`)
- `prisonId`\* (`string`)
- `sex` (`string`): `M`, `F`, or unknown.
- `name`\* (`AssertedProperName`):
  - `language`\* (`string` = code from [ISO 639-3](https://en.wikipedia.org/wiki/ISO_639-3), thesaurus: `person-name-languages`)
  - `tag` (`string`, optionally from thesaurus: `person-name-tags`): optional tag used to group names, typically used when a person has several names.
  - `parts`\* (`PersonNamePart[]`):
  - `type`\* (`string`, thesaurus: `person-name-types`): e.g. first name, last name, etc. Types are not unique in a name: for instance, you might have a person with 2 first names.
  - `value`\* (`string`)
  - assertion (`Assertion`)
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

## History

### 5.0.0

- 2023-11-19: ⚠️ upgraded to .NET 8.

### 4.0.0

- 2023-07-05: replaced graffiti IDs with `AssertedCompositeId`s.

### 3.0.3

- 2023-06-02: updated packages.

### 3.0.2

- 2023-05-19: updated packages.

### 3.0.1

- 2023-05-16: updated packages.

### 3.0.0

- 2023-03-16: upgraded to [new configuration](https://myrmex.github.io/overview/cadmus/dev/history/b-config/).

### 2.0.1

- 2022-11-28: updated packages.

### 2.0.0

- 2022-11-10: upgraded to NET 7.

### 1.4.0

- 2022-10-10: updated packages for new `IRepositoryProvider`.

### 1.3.2

- 2022-10-10: updated packages.

### 1.3.1

- 2022-10-05: updated packages and fixed legacy part references.

### 1.3.0

- 2022-09-27: replaced `ProperName` with AssertedProperName`.

### 1.2.2

- 2022-09-27: updated packages.

### 1.2.0

- 2022-07-30:
  - removed legacy Itinera dependencies.
  - replaced `PersonName` (from legacy prosopa bricks) with `ProperName` (from ref bricks). Note that this might be a breaking change for existing database records. In this case proper renaming should occur for proper name `pieces` (instead of `parts`).

### 1.1.4

- 2022-07-30: updated packages.

### 1.1.3

- 2021-12-22: updated packages.

- 2021-10-17: refactored DocReference now depending from bricks with a new model. This is a breaking change as it implies remodeling in the database parts of type `it.vedph.doc-references`: merge `author`, `work`, `location` into `citation` following some convention.
