# AIFC: AI File Classifier
![Logo](https://imgur.com/iGM4uAR.png)

[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)

## Authors
- **Marshall.codes**: [Linktree](https://linktr.ee/marshall.codes)

---

## Project Overview
AIFC (AI File Classifier) is a multi-component project designed to classify, extract, and preprocess structured data from various business-related files. This repository contains three main programs:

1. **ML Model Creation (C#)**: The core ML.NET-based program that builds a machine learning model for file classification (not yet implemented).
2. **Image Extractor Program (C#)**: A standalone tool utilizing PDFium and Tesseract OCR to extract images and text from PDF documents.
3. **Data Preprocessing Programs (Python)**:
   - **Dataset Preprocessing**: Prepares datasets from raw files by extracting, cleaning, and structuring data.
   - **Dataset Shuffling**: Randomizes data for better training outcomes.

Additionally, a closed proprietary software product will be launched based on this project.

---

### Data Folder
- **`RawData`**: Contains unprocessed files (e.g., PDFs).
- **`ExtractedData`**: Data extracted by the Image Extractor.
- **`StructuredData`**: Preprocessed data ready for use.

---

## Features
### Current Features:
1. **Machine Learning Model**: Uses ML.NET’s `SdcaMaximumEntropy` trainer for text classification (model only, classification program not yet implemented).
2. **Image and Text Extraction**: Leverages PDFium and Tesseract OCR to extract content from documents.
3. **Preprocessing Tools**: Python-based utilities for data preparation and shuffling.

### Future Enhancements:
1. **Integration of Ensemble Learning**:
   - Combine `SdcaMaximumEntropy`, LightGBM, and FastTree for improved accuracy.
2. **Hyperparameter Optimization**:
   - Implement grid search and random search for fine-tuning model performance.
3. **K-Fold Cross-Validation**:
   - Evaluate the model’s robustness across multiple folds.
4. **Dataset Expansion**:
   - Augment datasets for underrepresented classes.
5. **Automated Data Pipeline**:
   - Integrate Python preprocessing tools with the ML.NET pipeline.
6. **Implementation of File Classification Program**:
   - Develop the actual file classification program using the ML.NET model.

---

## Getting Started
### Prerequisites
- **Environment**:
  - .NET SDK 8.0+
  - Python 3.8+
  - PDFium and Tesseract OCR libraries
- **Dependencies**:
  - ML.NET NuGet packages
  - Python libraries: `pandas`, `numpy`, `scikit-learn`

### Running the Programs
1. **ML Model Creation**:
   - Command:
     ```bash
     dotnet run
     ```
2. **Image Extractor**:
   - Run the program separately to extract text and images.
   - Command:
     ```bash
     dotnet run --project ImageExtractor
     ```
3. **Preprocessing Programs**:
   - Execute Python scripts for dataset preprocessing and shuffling.
   - Commands:
     ```bash
     python Dataset_Preprocessing.py
     python Dataset_Shuffling.py
     ```

---

## Evaluation Metrics
- **Micro Accuracy**: Measures overall accuracy across all classes.
- **Macro Accuracy**: Averages accuracy for each class.
- **Log Loss**: Indicates model confidence.
- **Log Loss Reduction**: Higher values indicate better performance.

---

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch for your feature:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes and push:
   ```bash
   git commit -m "Add new feature"
   git push origin feature-name
   ```
4. Open a Pull Request on GitHub.

---

## License
This project is licensed under the [AGPL-3.0 License](https://www.gnu.org/licenses/agpl-3.0.html).