# Contribution Guide

## タスク管理

- [マイルストーン](https://github.com/d-maru/Strikelike/milestones)を達成していくことを目標にする
- 個々のタスクは [Issue](https://github.com/d-maru/Strikelike/issues)を作成し、関連するマイルストーンに紐づける

## コードの追加・修正

1. mainブランチでpullする
2. branchを切る
3. コードを書く
    - [C# のコーディング規則](https://docs.microsoft.com/ja-jp/dotnet/csharp/fundamentals/coding-style/coding-conventions#naming-conventions)になるべく従うこと
    - 厳密に守る必要はないが、できるだけ一行を100文字以下に収める
    - 用語や英単語に関しては[仕様書](specification/)で使われているものに統一する
4. commitする
5. pushする
6. Pull requestを作る
    - 関連するIssueがあればリンクを貼る
7. レビューしてもらう
8. mergeする

## ドキュメントの追加・修正

- `docs`ディレクトリ内に書く
- Pull requestは作っても作らなくてもいい
