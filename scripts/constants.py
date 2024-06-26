"""Contains constants used for the project"""

import pathlib

SOURCE_NAME = 'github'
NAMESPACE = 'OliverVea'

REPO_ROOT = pathlib.Path(__file__).parent.parent
SLN_FILE = REPO_ROOT / 'AotCommandLib.sln'
BUILD_ROOT = REPO_ROOT / 'build'
TEST_ROOT = REPO_ROOT / 'test'
SRC_ROOT = REPO_ROOT / 'src'

class Project:
    def __init__(self, name):
        self.name = name
        self.csproj_name = name + '.csproj'
        self.project_folder = SRC_ROOT / name
        self.csproj_path = self.project_folder / self.csproj_name

PROJECT = Project('AotCommandLib')
