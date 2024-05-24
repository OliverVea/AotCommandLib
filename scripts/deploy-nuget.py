"""Deploys the project as a nuget package to the Microsoft Nuget registry"""

import argparse
import datetime
import shutil

from constants import BUILD_ROOT, PROJECT
from commands.dotnet import pack, nuget_add_source, nuget_push

parser = argparse.ArgumentParser()

parser.add_argument('--prerelease', action='store_true')
parser.add_argument('--username', help='Github username', required=True, type=str)
parser.add_argument('--token', help='Github access token', required=True, type=str)
parser.add_argument('--cleanup', help='Removes build folder after deployment', action='store_true')

args = parser.parse_args()

config = 'Debug' if args.prerelease else 'Release'
out = BUILD_ROOT / PROJECT.name / config.lower()
version_suffix = f'p-{datetime.datetime.now().strftime("%Y-%m-%d-%H-%M-%S")}' if args.prerelease else None

if result := pack(
    project = PROJECT.csproj_path,
    out = out,
    configuration = config,
    include_symbols = args.prerelease,
    include_source = args.prerelease,
    version_suffix = version_suffix
):
    shutil.rmtree(BUILD_ROOT)
    exit(result)


if result := nuget_add_source(args.username, args.token):
    shutil.rmtree(BUILD_ROOT)
    exit(result)

nuget_path = out / '*.nupkg'

result = nuget_push(nuget_path, args.token)

if args.cleanup:
    shutil.rmtree(BUILD_ROOT)

exit(result)